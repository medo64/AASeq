using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Clamito {
    /// <summary>
    /// Protocol plugin.
    /// </summary>
    public sealed class ProtocolResolver {

        internal ProtocolResolver(Type type) {
            this.Type = type;

            var protocolAttributes = type.GetCustomAttributes(typeof(ProtocolAttribute), true);
            if (protocolAttributes.Length > 0) {
                var attr = (ProtocolAttribute)protocolAttributes[0];
                this.Model = attr.Model;
                if (!string.IsNullOrWhiteSpace(attr.Name)) { this.Name = attr.Name.Trim(); }
                if (!string.IsNullOrWhiteSpace(attr.DisplayName)) { this.DisplayName = attr.DisplayName.Trim(); }
                if (!string.IsNullOrWhiteSpace(attr.Description)) { this.Description = attr.Description.Trim(); }
            } else {
                this.Model = ProtocolModel.Peer;
            }

            if (this.Name == null) {
                this.Name = type.Name;
            }
            if (!ProtocolResolver.NameRegex.IsMatch(this.Name)) { throw new ArgumentOutOfRangeException("type", "Invalid protocol name."); }

            if (this.DisplayName == null) {
                var displayNameAttributes = type.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (displayNameAttributes.Length > 0) {
                    var attr = (DisplayNameAttribute)displayNameAttributes[0];
                    if (!string.IsNullOrWhiteSpace(attr.DisplayName)) { this.DisplayName = attr.DisplayName.Trim(); }
                }
            }
            if (this.DisplayName == null) {
                switch (this.Model) {
                    case ProtocolModel.Client: this.DisplayName = this.Name + " client"; break;
                    case ProtocolModel.Server: this.DisplayName = this.Name + " server"; break;
                    case ProtocolModel.Peer: this.DisplayName = this.Name + " peer"; break;
                    default: this.DisplayName = this.Name + " " + this.Model.ToString().ToLower(CultureInfo.CurrentCulture); break;
                }
            }

            if (this.Description == null) {
                var descriptionAttributes = type.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (descriptionAttributes.Length > 0) {
                    var attr = (DescriptionAttribute)descriptionAttributes[0];
                    if (!string.IsNullOrWhiteSpace(attr.Description)) { this.Description = attr.Description.Trim(); }
                }
            }
            if (this.Description == null) {
                this.Description = "";
            }
        }


        internal Type Type { get; private set; }


        /// <summary>
        /// Gets protocol model.
        /// </summary>
        public ProtocolModel Model { get; private set; }

        /// <summary>
        /// Gets protocol name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets protocol display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets protocol description.
        /// </summary>
        public string Description { get; private set; }


        /// <summary>
        /// Returns protocol proxy object.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Calling the method two times in succession creates different results.")]
        public ProtocolProxy GetProxy() {
            return new ProtocolProxy(this);
        }


        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object obj) {
            var other = obj as ProtocolResolver;
            var otherName = (other != null) ? other.Name : obj as string;
            return (otherName != null) && (ProtocolResolver.NameComparer.Compare(this.Name, otherName) == 0);
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            return this.DisplayName;
        }

        #endregion


        private static readonly Regex NameRegex = new Regex(@"^[\p{L}][\p{L}\p{Nd}_]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and underscores; must start with letter
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

    }
}
