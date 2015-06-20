using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Clamito {
    /// <summary>
    /// Message content.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Content is not a collection although it does implement some helper collection methods.")]
    public sealed class Content : IEnumerable<KeyValuePair<String, String>> {

        /// <summary>
        /// Create new instance.
        /// </summary>
        public Content()
            : this(new FieldCollection(), new FieldCollection()) {
        }

        /// <summary>
        /// Create new instance 
        /// </summary>
        /// <param name="header">Header fields.</param>
        /// <param name="data">Data fields.</param>
        /// <exception cref="System.ArgumentNullException">Header fields cannot be null. -or- Data fields cannot be null.</exception>
        public Content(FieldCollection header, FieldCollection data) {
            if (header == null) { throw new ArgumentNullException("header", "Header fields cannot be null."); }
            if (data == null) { throw new ArgumentNullException("data", "Data fields cannot be null."); }

            this.Header = header;
            this.Header.Changed += delegate(Object sender, EventArgs e) {
                this.OnChanged(new EventArgs());
            };

            this.Data = data;
            this.Data.Changed += delegate(Object sender, EventArgs e) {
                this.OnChanged(new EventArgs());
            };
        }


        /// <summary>
        /// Gets header field collection.
        /// </summary>
        public FieldCollection Header { get; private set; }

        /// <summary>
        /// Gets data field collection.
        /// </summary>
        public FieldCollection Data { get; private set; }


        #region Index

        /// <summary>
        /// Gets/sets item based on a path.
        /// Each subfield is separated by forward slash (/).
        /// Header fields are to be prefixed by at character (@).
        /// </summary>
        /// <param name="path">Name.</param>
        /// <exception cref="System.ArgumentNullException">Name path cannot be null.</exception>
        public string this[string path] {
            get {
                if (path == null) { throw new ArgumentNullException("path", "Name path cannot be null."); }

                var isHeader = path.StartsWith("@", StringComparison.Ordinal);
                var pathParts = (isHeader ? path.Substring(1) : path).Split(new char[] { '\\', '/' });

                var currFields = isHeader ? this.Header : this.Data;
                FieldCollection nextFields;
                for (int i = 0; i < pathParts.Length; i++) {
                    nextFields = null;
                    var name = pathParts[i];
                    foreach (var field in currFields) {
                        if (Field.NameComparer.Equals(field.Name, name)) {
                            if (i == pathParts.Length - 1) {
                                return field.Value;
                            } else {
                                nextFields = field.Subfields;
                                break;
                            }
                        }
                    }
                    if (nextFields != null) { currFields = nextFields; }
                }

                return null;
            }
            set {
                if (path == null) { throw new ArgumentNullException("path", "Name path cannot be null."); }

                var isHeader = path.StartsWith("@", StringComparison.Ordinal);
                var pathParts = (isHeader ? path.Substring(1) : path).Split(new char[] { '\\', '/' });

                var currFields = isHeader ? this.Header : this.Data;
                FieldCollection nextFields;
                for (int i = 0; i < pathParts.Length; i++) {
                    nextFields = null;
                    var name = pathParts[i];
                    foreach (var field in currFields) {
                        if (Field.NameComparer.Equals(field.Name, name)) {
                            if (i == pathParts.Length - 1) {
                                field.Value = value;
                                return;
                            } else {
                                nextFields = field.Subfields;
                                break;
                            }
                        }
                    }
                    if (nextFields != null) {
                        currFields = nextFields;
                    } else { //create that field
                        if (i == pathParts.Length - 1) {
                            currFields.Add(new Field(name, value));
                            return;
                        } else {
                            var newField = new Field(name);
                            currFields.Add(newField);
                            currFields = newField.Subfields;
                        }
                    }
                }
            }
        }

        #endregion


        #region IEnumerable

        /// <summary>
        /// Exposes the enumerator for iteration over each value field.
        /// Nested fields names will be separated by forward slash (/).
        /// Header fields will have prefix at (@).
        /// </summary>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
            foreach (var item in Content.EnumerateValues(this)) {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }


        /// <summary>
        /// Enumerates each value field.
        /// Nested fields names will be separated by forward slash (/).
        /// Header fields will have prefix at (@).
        /// </summary>
        /// <param name="content">Content.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "KeyValuePair is well known generic type and nesting it won't cause any major confusion.")]
        public static IEnumerable<KeyValuePair<string, string>> EnumerateValues(Content content) {
            foreach (var item in EnumerateFields(content.Header, isHeader: true)) {
                yield return item;
            }
            foreach (var item in EnumerateFields(content.Data)) {
                yield return item;
            }
        }

        /// <summary>
        /// Enumerates each value field.
        /// Nested fields names will be separated by forward slash (/).
        /// </summary>
        /// <param name="fields">Fields.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "KeyValuePair is well known generic type and nesting it won't cause any major confusion.")]
        public static IEnumerable<KeyValuePair<string, string>> EnumerateValues(FieldCollection fields) {
            foreach (var item in EnumerateFields(fields)) {
                yield return item;
            }
        }

        private static IEnumerable<KeyValuePair<string, string>> EnumerateFields(FieldCollection fields, bool isHeader = false, string pathPrefix = null) {
            foreach (var field in fields) {
                var path = (pathPrefix != null) ? pathPrefix + "/" + field.Name : (isHeader ? "@" + field.Name : field.Name);
                if (field.HasSubfields) {
                    foreach (var innerField in EnumerateFields(field.Subfields, pathPrefix: path)) {
                        yield return innerField;
                    }
                } else {
                    yield return new KeyValuePair<string, string>(path, field.Value);
                }
            }
        }

        #endregion


        #region Events

        /// <summary>
        /// Occurs when item changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Raises Changed event.
        /// </summary>
        /// <param name="e">Event data.</param>
        internal void OnChanged(EventArgs e) {
            var ev = this.Changed;
            if (ev != null) { ev(this, e); }
        }

        #endregion


        #region Convert to text

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            return ToString(this.Header, this.Data);
        }

        /// <summary>
        /// Returns a string that with headers and data combined.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <exception cref="System.ArgumentNullException">Message cannot be null.</exception>
        public static string ToString(Message message) {
            if (message == null) { throw new ArgumentNullException("message", "Message cannot be null."); }
            return ToString(message.Content.Header, message.Content.Data);
        }

        /// <summary>
        /// Returns a string representing header and data fields.
        /// Both header and data fields can be null.
        /// </summary>
        /// <param name="headerFields">Header fields.</param>
        /// <param name="dataFields">Data fields.</param>
        public static string ToString(FieldCollection headerFields, FieldCollection dataFields) {
            var sb = new StringBuilder();
            if (headerFields != null) { ToMultilineContentRecursion(sb, headerFields, 0, "@"); }
            if (dataFields != null) { ToMultilineContentRecursion(sb, dataFields, 0); }
            return sb.ToString();
        }


        private static void ToMultilineContentRecursion(StringBuilder sb, FieldCollection fields, int level, string prefix = null) {
            foreach (var field in fields) {
                sb.Append(new string(' ', level * 4));
                if (prefix != null) { sb.Append(prefix); }
                sb.Append(field.Name);
                AppendTags(sb, field);
                sb.Append(":");
                if (field.HasSubfields) {
                    sb.AppendLine();
                    ToMultilineContentRecursion(sb, field.Subfields, level + 1);
                } else {
                    if (!string.IsNullOrEmpty(field.Value)) {
                        sb.Append(" ");
                        var chars = field.Value;

                        int a;
                        for (a = 0; a < chars.Length; a++) {
                            if (chars[a] == ' ') {
                                sb.Append(@"\_");
                            } else {
                                break;
                            }
                        }

                        int b;
                        for (b = chars.Length - 1; b > a; b--) {
                            if (chars[b] != ' ') { break; }
                        }

                        for (int i = a; i <= b; i++) {
                            var ch = chars[i];
                            if (ch == '\t') {
                                sb.Append(@"\t");
                            } else if (ch == '\b') {
                                sb.Append(@"\b");
                            } else if (ch == '\n') {
                                sb.Append(@"\n");
                            } else if (ch == '\r') {
                                sb.Append(@"\r");
                            } else {
                                sb.Append(ch);
                            }
                        }
                        for (int i = b + 1; i < chars.Length; i++) {
                            sb.Append(@"\_");
                        }
                    }
                    sb.AppendLine();
                }
            }
        }

        private static void AppendTags(StringBuilder sb, Field field) {
            if (field.HasTags || field.HasModifiers) {
                sb.Append(" [");
                var first = true;
                foreach (var tag in field.Modifiers) {
                    if (first) { first = false; } else { sb.Append(" "); }
                    if (tag.State == false) { sb.Append("!"); }
                    sb.Append(".");
                    sb.Append(tag.Name);
                }
                foreach (var tag in field.Tags) {
                    if (first) { first = false; } else { sb.Append(" "); }
                    if (tag.State == false) { sb.Append("!"); }
                    sb.Append(tag.Name);
                }
                sb.Append("]");
            }
        }

        #endregion

        #region Convert from text

        /// <summary>
        /// Converts the specified string representation to its collection equivalent.
        /// All fields with @ character will be in header fields and all tags will be alphabetically sorted
        /// </summary>
        /// <param name="text">Content text.</param>
        /// <exception cref="System.ArgumentNullException">Text cannot be null.</exception>
        /// <exception cref="System.FormatException">Error parsing content.</exception>
        public static Content Parse(string text) {
            if (text == null) { throw new ArgumentNullException("text", "Text cannot be null."); }

            return Parse(text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None));
        }

        /// <summary>
        /// Converts the specified string representation to its collection equivalent.
        /// All fields with @ character will be in header fields and all tags will be alphabetically sorted
        /// </summary>
        /// <param name="lines">Content text.</param>
        /// <exception cref="System.ArgumentNullException">Lines cannot be null.</exception>
        /// <exception cref="System.FormatException">Error parsing content.</exception>
        public static Content Parse(IEnumerable<String> lines) {
            if (lines == null) { throw new ArgumentNullException("lines", "Lines cannot be null."); }

            var header = new List<string>();
            var data = new List<string>();

            var lastHeaderWhitespaceCount = -1;
            var wasHeader = false;
            foreach (var line in lines) {
                var isHeader = false;
                var hasAtChar = false;
                var whitespaceCount = 0;
                foreach (var ch in line) {
                    if (ch == '\t') {
                        whitespaceCount += 1000;
                    } else if (char.IsWhiteSpace(ch)) {
                        whitespaceCount += 1;
                    } else if (ch == '@') {
                        lastHeaderWhitespaceCount = whitespaceCount;
                        isHeader = true;
                        hasAtChar = true;
                        break;
                    } else {
                        if (wasHeader && (whitespaceCount > lastHeaderWhitespaceCount)) { isHeader = true; }
                        break;
                    }
                }

                if (isHeader) {
                    header.Add(hasAtChar ? line.Remove(line.IndexOf('@'), 1) : line);
                } else {
                    data.Add(line);
                }
                wasHeader = isHeader;
            }

            return new Content(FieldCollection.Parse(header), FieldCollection.Parse(data));
        }

        #endregion


        #region SetFields

        /// <summary>
        /// Parses multiline content and fills Field collections.
        /// </summary>
        /// <param name="text">Multiline content.</param>
        /// <exception cref="System.ArgumentNullException">Text cannot be null.</exception>
        /// <exception cref="System.FormatException">Error parsing content.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public void SetFields(string text) {
            if (text == null) { throw new ArgumentNullException("text", "Text cannot be null."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }

            var parsed = Content.Parse(text);
            this.SetFields(parsed.Header, parsed.Data);
        }

        /// <summary>
        /// Sets Field collections.
        /// </summary>
        /// <param name="header">Header fields.</param>
        /// <param name="data">Data fields.</param>
        /// <exception cref="System.ArgumentNullException">Header fields cannot be null. -or- Data fields cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public void SetFields(FieldCollection header, FieldCollection data) {
            if (header == null) { throw new ArgumentNullException("header", "Header fields cannot be null."); }
            if (data == null) { throw new ArgumentNullException("data", "Data fields cannot be null."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }

            this.Header.Clear(); //to release old stuff
            this.Data.Clear(); //to release old stuff

            this.Header = header;
            this.Data = data;
        }

        #endregion


        #region Clone

        private bool _isReadOnly;
        /// <summary>
        /// Gets a value indicating whether item is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return this._isReadOnly; }
            internal set { this._isReadOnly = value; }
        }


        /// <summary>
        /// Creates a copy of the content.
        /// </summary>
        public Content Clone() {
            return new Content(this.Header.Clone(), this.Data.Clone());
        }

        /// <summary>
        /// Creates a read-only copy of the message.
        /// </summary>
        public Content AsReadOnly() {
            var content = new Content(this.Header.AsReadOnly(), this.Data.AsReadOnly());
            content.IsReadOnly = true;
            return content;
        }

        #endregion

    }
}
