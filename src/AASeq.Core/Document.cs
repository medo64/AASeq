using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace AASeq;

/// <summary>
/// Main AASeq document.
/// </summary>
public sealed class Document {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public Document()
        : this(null, null) {
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="defaultEndpoints">Default endpoints.</param>
    /// <param name="defaultInteractions">Default interactions.</param>
    public Document(ICollection<Endpoint> defaultEndpoints, ICollection<Interaction> defaultInteractions) {
        Endpoints = new EndpointCollection(defaultEndpoints);
        Interactions = new InteractionCollection(defaultInteractions);

        Endpoints.Changed += Document_Changed;
        Interactions.Changed += Document_Changed;
    }


    /// <summary>
    /// Gets collection of endpoints.
    /// </summary>
    public EndpointCollection Endpoints { get; private set; }

    /// <summary>
    /// Gets collection of interactions.
    /// </summary>
    public InteractionCollection Interactions { get; private set; }


    /// <summary>
    /// Gets whether there are any endpoints in the document.
    /// </summary>
    public Boolean HasAnyEndpoints {
        get { return (Endpoints.Count > 0); }
    }

    /// <summary>
    /// Gets whether there are any interactions in the document.
    /// </summary>
    public Boolean HasAnyInteractions {
        get { return (Interactions.Count > 0); }
    }


    private bool _isChanged;
    /// <summary>
    /// Gets whether document was changed since last save.
    /// </summary>
    public bool IsChanged {
        get { return _isChanged; }
        private set {
            if (DontTrackChanges) { return; }
            if (_isChanged != value) {
                Log.Write.Verbose("Document", "IsChanged={0}", value);
                _isChanged = value;
                if (value) {
                    OnChanged(new EventArgs());
                }
            }
        }
    }


    #region Events

    /// <summary>
    /// Occurs when document changes.
    /// </summary>
    public event EventHandler Changed;

    /// <summary>
    /// Raises Changed event.
    /// </summary>
    /// <param name="e">Event data.</param>
    private void OnChanged(EventArgs e) {
        Log.Write.Verbose("Document", "OnChanged()");
        Changed?.Invoke(this, e);
    }

    #endregion


    #region Load/Save

    private static readonly Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

    #region Load

    /// <summary>
    /// Returns opened document.
    /// </summary>
    /// <param name="stream">Stream.</param>
    /// <exception cref="System.ArgumentNullException">Stream cannot be null.</exception>
    /// <exception cref="System.FormatException">Error parsing stream.</exception>
    public static Document Load(Stream stream) {
        if (stream == null) { throw new ArgumentNullException(nameof(stream), "Stream cannot be null."); }

        var sw = Stopwatch.StartNew();

        var doc = new Document() { DontTrackChanges = true };

        try {
            var xml = new XmlDocument();
            xml.Load(stream);

            var rootElement = xml.SelectSingleNode("/AASeq");
            if (rootElement == null) { throw new FormatException("Invalid root element."); }

            try {
                int minVersion = int.Parse(GetValue(rootElement.Attributes["minVersion"]), NumberStyles.Integer, CultureInfo.InvariantCulture);
                if (minVersion > 1) { throw new FormatException("Unsupported version number."); }
            } catch (ArgumentException) {
                throw new FormatException("Unsupported version number.");
            }

            foreach (XmlElement endpointElement in xml.SelectNodes("AASeq/Endpoints/Endpoint")) {
                var name = GetValue(endpointElement.Attributes["name"]);
                var caption = GetValue(endpointElement.Attributes["caption"]);
                var protocolName = GetValue(endpointElement.Attributes["protocolName"]);
                var newEndpoint = new Endpoint(name, protocolName) {
                    Caption = caption
                };
                doc.Endpoints.Add(newEndpoint);

                foreach (XmlElement fieldElement in endpointElement.SelectNodes("Field")) {
                    LoadFieldRecursive(fieldElement, newEndpoint.Data);
                }
            }

            foreach (XmlElement interactionElement in xml.SelectNodes("AASeq/Interactions/*")) {
                if (interactionElement.Name.Equals("Command", StringComparison.Ordinal)) {
                    var commandName = GetValue(interactionElement.Attributes["name"]);
                    var commandCaption = GetValue(interactionElement.Attributes["caption"]);
                    var command = new Command(commandName) {
                        Caption = commandCaption
                    };
                    foreach (XmlElement fieldElement in interactionElement.SelectNodes("Field")) {
                        LoadFieldRecursive(fieldElement, command.Data);
                    }
                    doc.Interactions.Add(command);
                } else if (interactionElement.Name.Equals("Message", StringComparison.Ordinal)) {
                    var messageName = GetValue(interactionElement.Attributes["name"]);
                    var messageCaption = GetValue(interactionElement.Attributes["caption"]);
                    var messageSource = doc.Endpoints[GetValue(interactionElement.Attributes["source"])];
                    var messageDestination = doc.Endpoints[GetValue(interactionElement.Attributes["destination"])];
                    var message = new Message(messageName, messageSource, messageDestination) {
                        Caption = messageCaption
                    };
                    foreach (XmlElement fieldElement in interactionElement.SelectNodes("Field")) {
                        LoadFieldRecursive(fieldElement, message.Data);
                    }
                    doc.Interactions.Add(message);
                }
            }

            doc.DontTrackChanges = false;
            return doc;

        } catch (FormatException ex) {
            Log.Write.Error("Document.Load", ex);
            throw;
        } catch (XmlException ex) {
            Log.Write.Error("Document.Load", ex);
            throw new FormatException(ex.Message);
        } catch (Exception ex) {
            Log.Write.Error("Document.Load", ex);
            throw;
        } finally {
            Log.Write.DocumentLoad(sw.ElapsedMilliseconds);
        }
    }

    private static void LoadFieldRecursive(XmlElement fieldElement, FieldCollection fieldCollection) {
        var name = GetValue(fieldElement.Attributes["name"]);
        var value = GetValue(fieldElement.Attributes["value"]);
        var tags = GetValue(fieldElement.Attributes["tags"]);
        var field = new Field(name);

        FillTags(tags, field.Tags);

        if (value != null) {
            field.Value = value;
        } else {
            foreach (XmlElement subfieldElement in fieldElement.SelectNodes("Field")) {
                LoadFieldRecursive(subfieldElement, field.Subfields);
            }
        }
        fieldCollection.Add(field);
    }

    private static void FillTags(string element, TagCollection list) {
        if (element != null) {
            foreach (string tag in element.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                var tagText = tag.Trim();
                if (tag.StartsWith("!", StringComparison.Ordinal)) {
                    list.Add(new Tag(tagText[1..].TrimStart(), false));
                } else {
                    list.Add(new Tag(tagText));
                }
            }
        }
    }

    #endregion

    #region Save

    /// <summary>
    /// Saves document.
    /// </summary>
    /// <param name="stream">Stream.</param>
    /// <exception cref="System.ArgumentNullException">Stream cannot be null.</exception>
    public void Save(Stream stream) {
        if (stream == null) { throw new ArgumentNullException(nameof(stream), "Stream cannot be null."); }

        var sw = Stopwatch.StartNew();

        try {
            using (var xw = new XmlTextWriter(stream, Document.encoding)) {
                xw.Formatting = Formatting.Indented;
                xw.Indentation = 4;

                xw.WriteStartDocument();
                xw.WriteStartElement("AASeq");
                xw.WriteAttributeString("version", "1");
                xw.WriteAttributeString("minVersion", "1");
                xw.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                xw.WriteAttributeString("xsi", "noNamespaceSchemaLocation", null, "https://api.aaseq.com/schema/aaseq.xsd");

                SaveEndpoints(xw);
                SaveFlows(xw);

                xw.WriteEndElement(); //AASeq
            }

            IsChanged = false;
        } catch (Exception ex) {
            Log.Write.Error("Document.Save", ex);
            throw;
        } finally {
            Log.Write.DocumentSave(sw.ElapsedMilliseconds);
        }
    }

    private void SaveEndpoints(XmlTextWriter xw) {
        xw.WriteStartElement("Endpoints");
        foreach (var endpoint in Endpoints) {
            xw.WriteStartElement("Endpoint");
            xw.WriteAttributeString("name", endpoint.Name);
            if (!string.IsNullOrEmpty(endpoint.Caption)) { xw.WriteAttributeString("caption", endpoint.Caption); }
            if (endpoint.ProtocolName != null) { xw.WriteAttributeString("protocolName", endpoint.ProtocolName); }

            foreach (var field in endpoint.Data) {
                SaveFieldRecursive(xw, field);
            }

            xw.WriteEndElement(); //Endpoint
        }
        xw.WriteEndElement(); //Endpoints
    }

    private void SaveFlows(XmlTextWriter xw) {
        xw.WriteStartElement("Interactions");
        foreach (var interaction in Interactions) {
            switch (interaction.Kind) {
                case InteractionKind.Command: {
                        var command = (Command)interaction;
                        xw.WriteStartElement("Command");
                        xw.WriteAttributeString("name", interaction.Name);
                        if (!string.IsNullOrEmpty(command.Caption)) { xw.WriteAttributeString("caption", command.Caption); }

                        if (command.HasData) {
                            foreach (var field in command.Data) {
                                SaveFieldRecursive(xw, field);
                            }
                        }

                        xw.WriteEndElement(); //Command
                    }
                    break;

                case InteractionKind.Message: {
                        var message = (Message)interaction;
                        xw.WriteStartElement("Message");
                        xw.WriteAttributeString("name", interaction.Name);
                        if (!string.IsNullOrEmpty(message.Caption)) { xw.WriteAttributeString("caption", message.Caption); }
                        if (message.Source != null) { xw.WriteAttributeString("source", message.Source.Name); }
                        if (message.Destination != null) { xw.WriteAttributeString("destination", message.Destination.Name); }

                        if (message.HasData) {
                            foreach (var field in message.Data) {
                                SaveFieldRecursive(xw, field);
                            }
                        }

                        xw.WriteEndElement(); //Message
                    }
                    break;
                default: throw new InvalidOperationException("Unknown interaction type.");
            }
        }
        xw.WriteEndElement(); //Interactions
    }

    private void SaveFieldRecursive(XmlTextWriter xw, Field field) {
        xw.WriteStartElement("Field");
        xw.WriteAttributeString("name", field.Name);
        if (field.HasTags) { SaveTags(xw, "tags", field.Tags); }
        if (field.HasValue) {
            xw.WriteAttributeString("value", field.Value);
        } else {
            foreach (var subfields in field.Subfields) {
                SaveFieldRecursive(xw, subfields);
            }
        }
        xw.WriteEndElement(); //Field
    }

    private static void SaveTags(XmlTextWriter xw, string attributeName, TagCollection tags) {
        var sbTag = new StringBuilder();
        foreach (var tag in tags) {
            if (sbTag.Length > 0) { sbTag.Append(' '); }
            if (tag.State) {
                sbTag.Append(tag.Name);
            } else {
                sbTag.Append("!" + tag.Name);
            }
        }
        xw.WriteAttributeString(attributeName, sbTag.ToString());
    }

    #endregion



    private static string GetValue(XmlAttribute attribute, string defaultValue = null) {
        return (attribute != null) ? attribute.Value : defaultValue;
    }

    #endregion


    private bool DontTrackChanges;

    private void Document_Changed(Object sender, EventArgs e) {
        IsChanged = true;
    }

}
