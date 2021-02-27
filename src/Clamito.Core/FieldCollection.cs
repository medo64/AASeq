using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Clamito {
    /// <summary>
    /// Collection of Fields.
    /// </summary>
    [DebuggerDisplay("{Count} fields")]
    public sealed class FieldCollection : IList<Field> {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public FieldCollection()
            : this(null) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="defaultItems">Default items of the collection.</param>
        public FieldCollection(ICollection<Field> defaultItems) {
            if (defaultItems != null) {
                foreach (var item in defaultItems) {
                    Add(item);
                }
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly List<Field> BaseCollection = new List<Field>();


        #region Index

        /// <summary>
        /// Gets/sets item value based on a path.
        /// Each subfield is separated by forward slash (/).
        /// </summary>
        /// <param name="path">Path.</param>
        /// <exception cref="System.ArgumentNullException">Path cannot be null.</exception>
        public string this[string path] {
            get {
                if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

                var pathParts = path.Split(new char[] { '\\', '/' });

                var currFields = this;
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
                if (path == null) { throw new ArgumentNullException(nameof(path), "Name path cannot be null."); }

                var pathParts = path.Split(new char[] { '\\', '/' });

                var currFields = this;
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

        /// <summary>
        /// Finds first matching item based on a path.
        /// Each subfield is separated by forward slash (/).
        /// </summary>
        /// <param name="path">Path.</param>
        /// <exception cref="System.ArgumentNullException">Path cannot be null.</exception>
        public Field FindFirst(string path) {
            if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

            var pathParts = path.Split(new char[] { '\\', '/' });

            var currFields = this;
            FieldCollection nextFields;
            for (int i = 0; i < pathParts.Length; i++) {
                nextFields = null;
                var name = pathParts[i];
                foreach (var field in currFields) {
                    if (Field.NameComparer.Equals(field.Name, name)) {
                        if (i == pathParts.Length - 1) {
                            return field;
                        } else {
                            nextFields = field.Subfields;
                            break;
                        }
                    }
                }
                if (nextFields != null) { currFields = nextFields; } else { break; }
            }

            return null;
        }

        /// <summary>
        /// Finds last matching item based on a path.
        /// Each subfield is separated by forward slash (/).
        /// </summary>
        /// <param name="path">Path.</param>
        /// <exception cref="System.ArgumentNullException">Path cannot be null.</exception>
        public Field FindLast(string path) {
            if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

            var pathParts = path.Split(new char[] { '\\', '/' });

            var currFields = this;
            Field lastField = null;
            FieldCollection nextFields;
            for (int i = 0; i < pathParts.Length; i++) {
                nextFields = null;
                var name = pathParts[i];
                foreach (var field in currFields) {
                    if (Field.NameComparer.Equals(field.Name, name)) {
                        if (i == pathParts.Length - 1) {
                            lastField = field;
                            nextFields = null;
                        } else {
                            nextFields = field.Subfields;
                            break;
                        }
                    }
                }
                if (lastField != null) { return lastField; }
                if (nextFields != null) { currFields = nextFields; } else { break; }
            }

            return null;
        }

        /// <summary>
        /// Finds all matching items based on a path.
        /// Each subfield is separated by forward slash (/).
        /// </summary>
        /// <param name="path">Path.</param>
        /// <exception cref="System.ArgumentNullException">Path cannot be null.</exception>
        public IEnumerable<Field> FindAll(string path) {
            if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

            var pathParts = path.Split(new char[] { '\\', '/' });

            var currFields = this;
            FieldCollection nextFields;
            for (int i = 0; i < pathParts.Length; i++) {
                nextFields = null;
                var name = pathParts[i];
                foreach (var field in currFields) {
                    if (Field.NameComparer.Equals(field.Name, name)) {
                        if (i == pathParts.Length - 1) {
                            yield return field;
                        } else {
                            nextFields = field.Subfields;
                            break;
                        }
                    }
                }
                if (nextFields != null) { currFields = nextFields; } else { break; }
            }
        }


        /// <summary>
        /// Adds new value field.
        /// </summary>
        /// <param name="path">Path. Components are separated by forward slash (/).</param>
        /// <param name="value">Value.</param>
        public void Add(string path, string value) {
            if (path == null) { throw new ArgumentNullException(nameof(path), "Name path cannot be null."); }

            var pathParts = path.Split(new char[] { '\\', '/' });

            var currFields = this;
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

        /// <summary>
        /// Determines whether the collection contains a specific item.
        /// </summary>
        /// <param name="name">Name of the item to locate.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        public bool Contains(string name) {
            foreach (var item in BaseCollection) {
                if (Field.NameComparer.Equals(item.Name, name)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the item from the collection.
        /// </summary>
        /// <param name="name">Name of the item to remove.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public bool Remove(string name) {
            if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }
            bool anyChanged = false;
            for (int i = BaseCollection.Count - 1; i >= 0; i--) {
                if (Field.NameComparer.Equals(BaseCollection[i].Name, name)) {
                    BaseCollection.RemoveAt(i);
                    anyChanged = true;
                }
            }
            if (anyChanged) {
                OnChanged(new EventArgs());
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Moves item in the collection
        /// </summary>
        /// <param name="moveFrom">Index of item's source.</param>
        /// <param name="moveTo">Index of item's destination.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Index out of range.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void MoveItem(int moveFrom, int moveTo) {
            if ((moveFrom < 0) || (moveFrom >= Count)) { throw new ArgumentOutOfRangeException(nameof(moveFrom), "Index out of range."); }
            if ((moveTo < 0) || (moveTo >= Count)) { throw new ArgumentOutOfRangeException(nameof(moveTo), "Index out of range."); }
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            if (moveFrom == moveTo) { return; }

            var item = BaseCollection[moveFrom];
            BaseCollection.RemoveAt(moveFrom);
            if (moveFrom > moveTo) { //move before
                BaseCollection.Insert(moveTo, item);
            } else { //move after
                BaseCollection.Insert(moveTo, item);
            }

            OnChanged(new EventArgs());
        }

        #endregion


        #region ICollection

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Add(Field item) {
            if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
            if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException(nameof(item), "Item cannot be in other collection."); }
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            item.OwnerCollection = this;
            BaseCollection.Add(item);
            OnChanged(new EventArgs());
        }

        /// <summary>
        /// Adds multiple items.
        /// </summary>
        /// <param name="items">Item.</param>
        /// <exception cref="System.ArgumentNullException">Items cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void AddRange(IEnumerable<Field> items) {
            if (items == null) { throw new ArgumentNullException(nameof(items), "Item cannot be null."); }
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            foreach (var item in items) {
                if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException(nameof(items), "Item cannot be in other collection."); }
                item.OwnerCollection = this;
                BaseCollection.Add(item);
            }

            OnChanged(new EventArgs());
        }

        /// <summary>
        /// Removes all items.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Clear() {
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            if (BaseCollection.Count > 0) {
                foreach (var item in BaseCollection) { item.OwnerCollection = null; }
                BaseCollection.Clear();
            }
            OnChanged(new EventArgs());
        }

        /// <summary>
        /// Determines whether the collection contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        public bool Contains(Field item) {
            if (item == null) { return false; }
            return BaseCollection.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Field[] array, int arrayIndex) {
            BaseCollection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items contained in the collection.
        /// </summary>
        public int Count {
            get { return BaseCollection.Count; }
        }

        /// <summary>
        /// Searches for the specified item and returns the zero-based index of the first occurrence within the entire collection.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        public int IndexOf(Field item) {
            return BaseCollection.IndexOf(item);
        }

        /// <summary>
        /// Inserts an element into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is greater than collection count. -or- Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Insert(int index, Field item) {
            if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
            if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException(nameof(item), "Item cannot be in other collection."); }
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            item.OwnerCollection = this;
            BaseCollection.Insert(index, item);
            OnChanged(new EventArgs());
        }

        private bool _isReadOnly;
        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return _isReadOnly; }
            private set { _isReadOnly = value; }
        }

        /// <summary>
        /// Removes the item from the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Item does not belong to this collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public bool Remove(Field item) {
            if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
            if (item.OwnerCollection != this) { throw new ArgumentOutOfRangeException(nameof(item), "Item does not belong to this collection."); }
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            if (BaseCollection.Remove(item)) {
                item.OwnerCollection = null;
                OnChanged(new EventArgs());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the element at the specified index of the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void RemoveAt(int index) {
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            var item = this[index];
            item.OwnerCollection = null;
            BaseCollection.Remove(item);
            OnChanged(new EventArgs());
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
        /// </summary>
        public IEnumerator<Field> GetEnumerator() {
            return BaseCollection.GetEnumerator();
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        public IEnumerator<Field> GetEnumerator(string name) {
            foreach (var item in BaseCollection) {
                if (Field.NameComparer.Equals(item.Name, name)) {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a non-generic collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() {
            return BaseCollection.GetEnumerator();
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public Field this[int index] {
            get { return BaseCollection[index]; }
            set {
                if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

                var item = this[index];

                item.OwnerCollection = null;
                BaseCollection.RemoveAt(index);

                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this[item.Name] != null) { throw new ArgumentOutOfRangeException(nameof(value), "Duplicate name in collection."); }
                if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException(nameof(value), "Item cannot be in other collection."); }

                item.OwnerCollection = this;
                BaseCollection.Insert(index, item);
                OnChanged(new EventArgs());
            }
        }

        #endregion


        #region Clone

        /// <summary>
        /// Creates a copy of the collection.
        /// </summary>
        public FieldCollection Clone() {
            var collection = new FieldCollection();
            foreach (var item in this) {
                collection.Add(item.Clone());
            }
            return collection;
        }

        /// <summary>
        /// Creates a read-only copy of the collection.
        /// </summary>
        public FieldCollection AsReadOnly() {
            var collection = new FieldCollection();
            foreach (var item in this) {
                collection.Add(item.AsReadOnly());
            }
            collection.IsReadOnly = true;
            return collection;
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
            var ev = Changed;
            if (ev != null) { ev(this, e); }
        }

        #endregion


        #region Convert to text

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            var sb = new StringBuilder();
            ToMultilineContentRecursion(sb, this, 0);
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
            if (field.HasTags) {
                sb.Append(" [");
                var first = true;
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
        /// </summary>
        /// <param name="text">Content text.</param>
        /// <exception cref="System.ArgumentNullException">Text cannot be null.</exception>
        /// <exception cref="System.FormatException">Error parsing content.</exception>
        public static FieldCollection Parse(string text) {
            if (text == null) { throw new ArgumentNullException(nameof(text), "Text cannot be null."); }

            return Parse(text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None));
        }

        /// <summary>
        /// Converts the specified string representation to its collection equivalent.
        /// Fields with dot (.) are ordered before other fields.
        /// </summary>
        /// <param name="lines">Content lines.</param>
        /// <exception cref="System.ArgumentNullException">Lines cannot be null.</exception>
        /// <exception cref="System.FormatException">Error parsing content.</exception>
        public static FieldCollection Parse(IEnumerable<String> lines) {
            if (lines == null) { throw new ArgumentNullException(nameof(lines), "Lines cannot be null."); }

            //split lines
            var entries = new List<ContentLine>();
            var lineNumber = 0;
            try {
                foreach (var line in lines) {
                    lineNumber += 1;
                    var trimmedLine = line.TrimEnd(); //don't trim start because we count that whitespace
                    if (!string.IsNullOrEmpty(trimmedLine)) {
                        var entry = new ContentLine(lineNumber, trimmedLine);
                        entries.Add(entry);
                    }
                }

            } catch (Exception ex) {
                var newEx = new FormatException(ex.Message.Split('\r', '\n')[0], ex); //just take first line from exception
                if (lineNumber > 0) { newEx.Data.Add("Line", lineNumber); }
                throw newEx;
            }


            if (entries.Count > 0) {
                var owners = new Stack<ContentLine>();
                ContentLine lastEntry = entries[0];
                foreach (var entry in entries) {
                    if (entry.WhitespaceCount == lastEntry.WhitespaceCount) {
                        entry.Owner = (owners.Count > 0) ? owners.Peek() : null;
                    } else if (entry.WhitespaceCount > lastEntry.WhitespaceCount) {
                        owners.Push(lastEntry);
                        entry.Owner = lastEntry;
                    } else if (entry.WhitespaceCount < lastEntry.WhitespaceCount) {
                        while (owners.Count > 0) {
                            var popped = owners.Pop();
                            if (popped.WhitespaceCount < entry.WhitespaceCount) {
                                entry.Owner = popped;
                                break;
                            }
                        }
                    }
                    lastEntry = entry;
                }
            }

            var newFields = new List<Field>();
            FromMultilineContentRecursion(entries, null, newFields);

            return new FieldCollection(newFields);
        }


        private static void FromMultilineContentRecursion(IEnumerable<ContentLine> entries, ContentLine owner, IList<Field> fields) {
            var newHeaderFields = new List<Field>();
            var newFields = new List<Field>();

            foreach (var entry in entries) {
                if (entry.Owner == owner) {
                    Field newField;
                    try {
                        newField = new Field(entry.Name, entry.Value);
                        foreach (var tag in entry.Tags) {
                            if (!newField.Tags.Contains(tag.Name)) {
                                newField.Tags.Add(tag);
                            }
                        }
                        if (newField.IsModifier) {
                            newHeaderFields.Add(newField);
                        } else {
                            newFields.Add(newField);
                        }
                    } catch (Exception ex) {
                        var newEx = new FormatException(ex.Message.Split('\r', '\n')[0], ex); //just take first line from exception
                        if (entry.LineNumber > 0) { newEx.Data.Add("Line", entry.LineNumber); }
                        throw newEx;
                    }
                    FromMultilineContentRecursion(entries, entry, newField.Subfields);
                }
            }
            foreach (var field in newHeaderFields) { fields.Add(field); } //order headers first
            foreach (var field in newFields) { fields.Add(field); }
        }


        [DebuggerDisplay("{Name}: {Value}")]
        private class ContentLine {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Complexity is high because of state machine. While it could be split in multiple methods, I do not think that this would reduce complexity.")]
            public ContentLine(int lineNumber, string line) {
                LineNumber = lineNumber;

                var whitespaceCount = 0;
                var state = State.Whitespace;
                var name = new StringBuilder();
                var value = new StringBuilder();
                var tag = new StringBuilder();
                var tags = new List<string>();

                foreach (var ch in line) {
                    switch (state) {
                        case State.Whitespace:
                            {
                                if (ch == '\t') {
                                    whitespaceCount += 1000;
                                } else if (char.IsWhiteSpace(ch)) {
                                    whitespaceCount += 1;
                                } else if (ch == '[') {
                                    state = State.Tag;
                                } else {
                                    name.Append(ch);
                                    state = State.Name;
                                }
                            }
                            break;

                        case State.BeforeName:
                            {
                                if (ch == '\t') {
                                } else if (char.IsWhiteSpace(ch)) {
                                } else if (ch == '[') {
                                    state = State.Tag;
                                } else {
                                    name.Append(ch);
                                    state = State.Name;
                                }
                            }
                            break;

                        case State.Name:
                            {
                                if (char.IsWhiteSpace(ch)) {
                                    state = State.AfterName;
                                } else if (ch == '[') {
                                    state = State.Tag;
                                } else if ((ch == ':') || (ch == '=')) {
                                    state = State.BeforeValue;
                                } else {
                                    name.Append(ch);
                                }
                            }
                            break;

                        case State.AfterName:
                            {
                                if (char.IsWhiteSpace(ch)) {
                                } else if (ch == '\\') {
                                    state = State.ValueLiteral;
                                } else if (ch == '[') {
                                    state = State.Tag;
                                } else if ((ch == ':') || (ch == '=')) {
                                    state = State.BeforeValue;
                                } else { //assume rest is value
                                    value.Append(ch);
                                    state = State.Value;
                                }
                            }
                            break;

                        case State.Tag:
                            {
                                if (char.IsWhiteSpace(ch) || (ch == ',') || (ch == ';')) {
                                    var tagText = tag.ToString().Trim();
                                    if (!string.IsNullOrEmpty(tagText)) { tags.Add(tagText); }
                                    tag.Clear();
                                } else if (ch == ']') {
                                    var tagText = tag.ToString().Trim();
                                    if (!string.IsNullOrEmpty(tagText)) { tags.Add(tagText); }
                                    tag.Clear();
                                    if (name.Length > 0) {
                                        state = State.AfterName;
                                    } else {
                                        state = State.BeforeName;
                                    }
                                } else if ((ch == ':') || (ch == '=')) {
                                    var tagText = tag.ToString().Trim();
                                    if (!string.IsNullOrEmpty(tagText)) { tags.Add(tagText); }
                                    tag.Clear();
                                    state = State.BeforeValue;
                                } else {
                                    tag.Append(ch);
                                }
                            }
                            break;

                        case State.BeforeValue:
                            {
                                if (char.IsWhiteSpace(ch)) {
                                } else if (ch == '\\') {
                                    state = State.ValueLiteral;
                                } else { //assume rest is value
                                    value.Append(ch);
                                    state = State.Value;
                                }
                            }
                            break;

                        case State.Value:
                            {
                                if (ch == '\\') {
                                    state = State.ValueLiteral;
                                } else {
                                    value.Append(ch);
                                }
                            }
                            break;

                        case State.ValueLiteral:
                            {
                                if (ch == '_') {
                                    value.Append(" ");
                                } else if (ch == 't') {
                                    value.Append("\t");
                                } else if (ch == 'b') {
                                    value.Append("\b");
                                } else if (ch == 'n') {
                                    value.Append("\n");
                                } else if (ch == 'r') {
                                    value.Append("\r");
                                } else { //just take it as it is
                                    value.Append(ch);
                                }
                                state = State.Value;
                            }
                            break;

                    }
                }

                Name = name.ToString();
                Value = value.ToString();
                WhitespaceCount = whitespaceCount;

                Tags = ProcessTags(tags);
            }

            private static IEnumerable<Tag> ProcessTags(List<string> tags) {
                var modifierList = new List<Tag>();
                var tagList = new List<Tag>();
                foreach (var tagName in tags) {
                    if (tagName.StartsWith("!.", StringComparison.Ordinal) || tagName.StartsWith("-.", StringComparison.Ordinal) || tagName.StartsWith(".!", StringComparison.Ordinal) || tagName.StartsWith(".-", StringComparison.Ordinal)) {
                        modifierList.Add(new Tag("." + tagName.Substring(2), false));
                    } else if (tagName.StartsWith("+.", StringComparison.Ordinal) || tagName.StartsWith(".+", StringComparison.Ordinal)) {
                        modifierList.Add(new Tag("." + tagName.Substring(2), true));
                    } else if (tagName.StartsWith(".", StringComparison.Ordinal) && tagName.EndsWith("-", StringComparison.Ordinal)) {
                        modifierList.Add(new Tag(tagName.Substring(0, tagName.Length - 1), false));
                    } else if (tagName.StartsWith(".", StringComparison.Ordinal) && tagName.EndsWith("+", StringComparison.Ordinal)) {
                        modifierList.Add(new Tag(tagName.Substring(0, tagName.Length - 1), true));
                    } else if (tagName.StartsWith(".", StringComparison.Ordinal)) {
                        modifierList.Add(new Tag(tagName, true));
                    } else if (tagName.StartsWith("!", StringComparison.Ordinal) || tagName.StartsWith("-", StringComparison.Ordinal)) {
                        tagList.Add(new Tag(tagName.Substring(1), false));
                    } else if (tagName.StartsWith("+", StringComparison.Ordinal)) {
                        tagList.Add(new Tag(tagName.Substring(1), true));
                    } else if (tagName.EndsWith("-", StringComparison.Ordinal)) {
                        tagList.Add(new Tag(tagName.Substring(0, tagName.Length - 1), false));
                    } else if (tagName.EndsWith("+", StringComparison.Ordinal)) {
                        tagList.Add(new Tag(tagName.Substring(0, tagName.Length - 1), true));
                    } else {
                        tagList.Add(new Tag(tagName, true));
                    }
                }

                modifierList.Sort(
                    delegate (Tag item1, Tag item2) {
                        return string.CompareOrdinal(item1.Name, item2.Name);
                    }
                );
                tagList.Sort(
                    delegate (Tag item1, Tag item2) {
                        return string.CompareOrdinal(item1.Name, item2.Name);
                    }
                );

                foreach (var tag in modifierList) {
                    yield return tag;
                }
                foreach (var tag in tagList) {
                    yield return tag;
                }
            }

            public int LineNumber { get; private set; }
            public int WhitespaceCount { get; private set; }
            public string Name { get; private set; }
            public string Value { get; private set; }
            public IEnumerable<Tag> Tags { get; private set; }

            public ContentLine Owner { get; set; }

            private enum State {
                Whitespace,
                BeforeName,
                Name,
                AfterName,
                Tag,
                BeforeValue,
                Value,
                ValueLiteral
            }
        }

        #endregion


        #region Paths

        /// <summary>
        /// Exposes the enumerator for iteration over each field.
        /// Nested fields names will be separated by forward slash (/).
        /// </summary>
        public IEnumerable<FieldNode> AllPaths {
            get {
                foreach (var item in EnumerateAllPaths(this)) {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Exposes the enumerator for iteration over each value field.
        /// Nested fields names will be separated by forward slash (/).
        /// </summary>
        public IEnumerable<FieldNode> PathsWithValue {
            get {
                foreach (var item in AllPaths) {
                    if (item.Field.HasValue) { yield return item; }
                }
            }
        }


        private static IEnumerable<FieldNode> EnumerateAllPaths(FieldCollection fields, string pathPrefix = null) {
            foreach (var field in fields) {
                var path = ((pathPrefix != null) ? pathPrefix + "/" : "") + field.Name;
                yield return new FieldNode(path, field);
                if (field.HasSubfields) {
                    foreach (var innerField in EnumerateAllPaths(field.Subfields, pathPrefix: path)) {
                        yield return innerField;
                    }
                }
            }
        }

        #endregion

    }
}
