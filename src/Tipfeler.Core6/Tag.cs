using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Tipfeler {

    /// <summary>
    /// Tag.
    /// </summary>
    [DebuggerDisplay("{Name} = {State}")]
    public sealed class Tag {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="state">State.</param>
        /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Tag(string name, bool state) {
            if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
            if (!IsNameValid(name)) { throw new ArgumentOutOfRangeException(nameof(name), "Name contains invalid characters."); }
            Name = name;
            State = state;
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Tag(string name)
            : this(name, true) {
        }


        /// <summary>
        /// Gets name.
        /// </summary>
        public string Name { get; private init; }

        private bool _state;
        /// <summary>
        /// Gets/sets state for a tag.
        /// </summary>
        public bool State {
            get { return _state; }
            set {
                if (_state != value) {
                    _state = value;
                    OnChanged(new EventArgs());
                }
            }
        }


        #region Events

        /// <summary>
        /// Occurs when item changes.
        /// </summary>
        public event EventHandler<EventArgs>? Changed;

        /// <summary>
        /// Raises Changed event.
        /// </summary>
        /// <param name="e">Event data.</param>
        internal void OnChanged(EventArgs e) {
            Changed?.Invoke(this, e);
        }

        #endregion


        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object? obj) {
            return (obj is Tag other) && IsSameName(other.Name) && (State == other.State);
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        public override int GetHashCode() {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            return State ? Name : "!" + Name;
        }

        #endregion



        /// <summary>
        /// Returns true if name is valid.
        /// </summary>
        /// <param name="name">Name.</param>
        public static bool IsNameValid(string name) {
            return (name != null) && NameRegex.IsMatch(name);
        }

        /// <summary>
        /// Returns if name matches.
        /// </summary>
        /// <param name="name">Name to match.</param>
        public bool IsSameName(string name) {
            return (name != null) && (NameComparer.Compare(Name, name) == 0);
        }

        private static readonly Regex NameRegex = new(@"^@?\p{L}[\p{L}\p{Nd}]*$");  // allowed only letters and numbers; can start with at sign (@)
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

    }
}
