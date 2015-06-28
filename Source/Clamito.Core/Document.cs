using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Clamito {

    /// <summary>
    /// Main Clamito document.
    /// </summary>
    public sealed class Document {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public Document()
            : this(null, null, null) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="defaultEndpoints">Default endpoints.</param>
        /// <param name="defaultInteractions">Default interactions.</param>
        /// <param name="defaultVariables">Default variables.</param>
        public Document(ICollection<Endpoint> defaultEndpoints, ICollection<Interaction> defaultInteractions, ICollection<Field> defaultVariables) {
            this.Endpoints = new EndpointCollection(defaultEndpoints);
            this.Interactions = new InteractionCollection(defaultInteractions);
            this.Variables = new FieldCollection(defaultVariables);

            this.Endpoints.Changed += Document_Changed;
            this.Interactions.Changed += Document_Changed;
            this.Variables.Changed += Document_Changed;
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
        /// Gets collection of variables.
        /// </summary>
        public FieldCollection Variables { get; private set; }


        /// <summary>
        /// Gets whether there are any endpoints in the document.
        /// </summary>
        public Boolean HasAnyEndpoints {
            get { return (this.Endpoints.Count > 0); }
        }

        /// <summary>
        /// Gets whether there are any interactions in the document.
        /// </summary>
        public Boolean HasAnyInteractions {
            get { return (this.Interactions.Count > 0); }
        }

        /// <summary>
        /// Gets whether there are any variables in the document.
        /// </summary>
        public Boolean HasAnyVariables {
            get { return (this.Variables.Count > 0); }
        }


        /// <summary>
        /// Gets last used file name in open/save operations.
        /// It will be null if stream was used for that operation.
        /// </summary>
        public string FileName { get; private set; }


        private bool _isChanged;
        /// <summary>
        /// Gets whether document was changed since last save.
        /// </summary>
        public bool IsChanged {
            get { return this._isChanged; }
            private set {
                if (this.DontTrackChanges) { return; }
                if (this._isChanged != value) {
                    Log.WriteVerbose("Document.IsChanged={0}", value);
                    this._isChanged = value;
                    if (value) {
                        this.OnChanged(new EventArgs());
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
            Log.WriteVerbose("Document.OnChanged");
            var ev = this.Changed;
            if (ev != null) { ev(this, e); }
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
            if (stream == null) { throw new ArgumentNullException("stream", "Stream cannot be null."); }

            var sw = Stopwatch.StartNew();

            var doc = new Document() { DontTrackChanges = true };

            try {
                var xml = new XmlDocument();
                xml.Load(stream);

                var rootElement = xml.SelectSingleNode("/Clamito");
                if (rootElement == null) { throw new FormatException("Invalid root element."); }

                try {
                    int minVersion = int.Parse(GetValue(rootElement.Attributes["minVersion"]), NumberStyles.Integer, CultureInfo.InvariantCulture);
                    if (minVersion > 1) { throw new FormatException("Unsupported version number."); }
                } catch (ArgumentException) {
                    throw new FormatException("Unsupported version number.");
                }

                foreach (XmlElement endpointElement in xml.SelectNodes("Clamito/Endpoints/Endpoint")) {
                    var name = GetValue(endpointElement.Attributes["name"]);
                    var protocolName = GetValue(endpointElement.Attributes["protocolName"]);
                    var displayName = GetValue(endpointElement.Attributes["displayName"]);
                    var description = GetValue(endpointElement.Attributes["description"]);
                    var newEndpoint = new Endpoint(name, protocolName) {
                        DisplayName = displayName,
                        Description = description
                    };
                    doc.Endpoints.Add(newEndpoint);

                    foreach (XmlElement fieldElement in endpointElement.SelectNodes("Property")) {
                        LoadFieldRecursive(fieldElement, "Property", newEndpoint.Properties);
                    }
                }

                foreach (XmlElement interactionElement in xml.SelectNodes("Clamito/Interactions/*")) {
                    if (interactionElement.Name.Equals("Command", StringComparison.Ordinal)) {
                        var commandName = GetValue(interactionElement.Attributes["name"]);
                        var commandParameters = GetValue(interactionElement.Attributes["parameters"]);
                        var commandDescription = GetValue(interactionElement.Attributes["description"]);
                        var command = new Command(commandName, commandParameters) {
                            Description = commandDescription
                        };
                        doc.Interactions.Add(command);
                    } else if (interactionElement.Name.Equals("Message", StringComparison.Ordinal)) {
                        var messageName = GetValue(interactionElement.Attributes["name"]);
                        var messageSource = doc.Endpoints[GetValue(interactionElement.Attributes["source"])];
                        var messageDestination = doc.Endpoints[GetValue(interactionElement.Attributes["destination"])];
                        var messageDescription = GetValue(interactionElement.Attributes["description"]);
                        var message = new Message(messageName, messageSource, messageDestination) {
                            Description = messageDescription
                        };
                        doc.Interactions.Add(message);

                        foreach (XmlElement fieldsElement in interactionElement.SelectNodes("Fields")) {
                            foreach (XmlElement fieldElement in fieldsElement.SelectNodes("Field")) {
                                LoadFieldRecursive(fieldElement, "Field", message.Fields);
                            }
                        }
                    }
                }

                foreach (XmlElement variableElement in xml.SelectNodes("Clamito/Variables/Variable")) {
                    LoadFieldRecursive(variableElement, "Variable", doc.Variables);
                }

                doc.DontTrackChanges = false;
                return doc;

            } catch (FormatException ex) {
                Log.WriteException("Document.Load", ex);
                throw;
            } catch (XmlException ex) {
                Log.WriteException("Document.Load", ex);
                throw new FormatException(ex.Message);
            } catch (Exception ex) {
                Log.WriteException("Document.Load", ex);
                throw;
            } finally {
                Log.RecordDocumentLoad(sw.ElapsedMilliseconds);
            }
        }

        private static void LoadFieldRecursive(XmlElement fieldElement, string nodeName, FieldCollection fieldCollection) {
            var name = GetValue(fieldElement.Attributes["name"]);
            var value = GetValue(fieldElement.Attributes["value"]);
            var tags = GetValue(fieldElement.Attributes["tags"]);
            var field = new Field(name);

            FillTags(tags, field.Tags);

            if (value != null) {
                field.Value = value;
            } else {
                foreach (XmlElement subfieldElement in fieldElement.SelectNodes(nodeName)) {
                    LoadFieldRecursive(subfieldElement, nodeName, field.Subfields);
                }
            }
            fieldCollection.Add(field);
        }

        private static void FillTags(string element, TagCollection list) {
            if (element != null) {
                foreach (string tag in element.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                    var tagText = tag.Trim();
                    if (tag.StartsWith("!", StringComparison.Ordinal)) {
                        list.Add(new Tag(tagText.Substring(1).TrimStart(), false));
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
            if (stream == null) { throw new ArgumentNullException("stream", "Stream cannot be null."); }

            var sw = Stopwatch.StartNew();

            try {
                using (var xw = new XmlTextWriter(stream, Document.encoding)) {
                    xw.Formatting = Formatting.Indented;
                    xw.Indentation = 4;

                    xw.WriteStartDocument();
                    xw.WriteStartElement("Clamito");
                    xw.WriteAttributeString("version", "1");
                    xw.WriteAttributeString("minVersion", "1");
                    xw.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                    xw.WriteAttributeString("xsi", "noNamespaceSchemaLocation", null, "http://jmedved.com/schema/clamito.xsd");

                    SaveEndpoints(xw);
                    SaveFlows(xw);
                    SaveVariables(xw);

                    xw.WriteEndElement(); //Clamito
                }

                this.IsChanged = false;
                this.FileName = null;

            } catch (Exception ex) {
                Log.WriteException("Document.Save", ex);
                throw;
            } finally {
                Log.RecordDocumentSave(sw.ElapsedMilliseconds);
            }
        }

        private void SaveEndpoints(XmlTextWriter xw) {
            xw.WriteStartElement("Endpoints");
            foreach (var endpoint in this.Endpoints) {
                xw.WriteStartElement("Endpoint");
                xw.WriteAttributeString("name", endpoint.Name);
                xw.WriteAttributeString("displayName", endpoint.DisplayName);
                if (endpoint.ProtocolName != null) { xw.WriteAttributeString("protocolName", endpoint.ProtocolName); }
                if (!string.IsNullOrEmpty(endpoint.Description)) { xw.WriteAttributeString("description", endpoint.Description); }

                foreach (var field in endpoint.Properties) {
                    SaveFieldRecursive(xw, "Property", field);
                }

                xw.WriteEndElement(); //Endpoint
            }
            xw.WriteEndElement(); //Endpoints
        }

        private void SaveFlows(XmlTextWriter xw) {
            xw.WriteStartElement("Interactions");
            foreach (var interaction in this.Interactions) {
                switch (interaction.Kind) {
                    case InteractionKind.Command:
                        {
                            var command = (Command)interaction;
                            xw.WriteStartElement("Command");
                            xw.WriteAttributeString("name", interaction.Name);
                            if (!string.IsNullOrEmpty(command.Parameters)) { xw.WriteAttributeString("parameters", command.Parameters); }
                            if (!string.IsNullOrEmpty(command.Description)) { xw.WriteAttributeString("description", command.Description); }
                            xw.WriteEndElement(); //Command
                        }
                        break;

                    case InteractionKind.Message:
                        {
                            var message = (Message)interaction;
                            xw.WriteStartElement("Message");
                            xw.WriteAttributeString("name", interaction.Name);
                            if (message.Source != null) { xw.WriteAttributeString("source", message.Source.Name); }
                            if (message.Destination != null) { xw.WriteAttributeString("destination", message.Destination.Name); }
                            if (!string.IsNullOrEmpty(message.Description)) { xw.WriteAttributeString("description", message.Description); }

                            if (message.HasFields) {
                                xw.WriteStartElement("Fields");
                                foreach (var field in message.Fields) {
                                    SaveFieldRecursive(xw, "Field", field);
                                }
                                xw.WriteEndElement(); //Fields
                            }

                            xw.WriteEndElement(); //Message
                        }
                        break;
                    default: throw new InvalidOperationException("Unknown interaction type.");
                }
            }
            xw.WriteEndElement(); //Interactions
        }

        private void SaveFieldRecursive(XmlTextWriter xw, String nodeName, Field field) {
            xw.WriteStartElement(nodeName);
            xw.WriteAttributeString("name", field.Name);
            if (field.HasTags) { SaveTags(xw, "tags", field.Tags); }
            if (field.HasValue) {
                xw.WriteAttributeString("value", field.Value);
            } else {
                foreach (var subfields in field.Subfields) {
                    SaveFieldRecursive(xw, nodeName, subfields);
                }
            }
            xw.WriteEndElement(); //Field
        }

        private static void SaveTags(XmlTextWriter xw, string attributeName, TagCollection tags) {
            var sbTag = new StringBuilder();
            foreach (var tag in tags) {
                if (sbTag.Length > 0) { sbTag.Append(" "); }
                if (tag.State) {
                    sbTag.Append(tag.Name);
                } else {
                    sbTag.Append("!" + tag.Name);
                }
            }
            xw.WriteAttributeString(attributeName, sbTag.ToString());
        }

        private void SaveVariables(XmlTextWriter xw) {
            xw.WriteStartElement("Variables");
            foreach (var field in this.Variables) {
                SaveFieldRecursive(xw, "Variable", field);
            }
            xw.WriteEndElement(); //Variables
        }

        #endregion



        private static string GetValue(XmlAttribute attribute, string defaultValue = null) {
            return (attribute != null) ? attribute.Value : defaultValue;
        }

        #endregion


        private bool DontTrackChanges;

        private void Document_Changed(Object sender, EventArgs e) {
            this.IsChanged = true;
        }

    }
}
