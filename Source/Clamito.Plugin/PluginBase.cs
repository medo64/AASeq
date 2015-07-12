using System;

namespace Clamito {

    /// <summary>
    /// Plugin interface.
    /// </summary>
    public abstract class PluginBase : IDisposable {

        #region Definition

        /// <summary>
        /// Gets unique name for plugin.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets display name for plugin.
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets plugin description.
        /// </summary>
        public abstract string Description { get; }

        #endregion


        #region IDisposable

        /// <summary>
        /// Releases all non-managed resources.
        /// </summary>
        ~PluginBase() {
            this.Dispose(false);
        }

        /// <summary>
        /// Releases all allocated resources.
        /// </summary>
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all allocated resources.
        /// </summary>
        /// <param name="disposing">True if disposing managed objects.</param>
        protected virtual void Dispose(bool disposing) {
            //override this method if disposal is needed
        }

        #endregion


        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object obj) {
            var other = obj as PluginBase;
            if (other != null) { return (obj.GetType().Equals(this.GetType())) && this.Name.Equals(other.Name); }

            var otherString = obj as string;
            if (otherString != null) { return this.Name.Equals(otherString); }

            return false;
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

    }
}
