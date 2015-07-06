using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Clamito {
    /// <summary>
    /// Result collection.
    /// </summary>
    [DebuggerDisplay("IsSuccess={IsSuccess}; {Count} results")]
    public class ResultCollection : IList<ErrorResult> {

        /// <summary>
        /// Create new instance.
        /// </summary>
        public ResultCollection()
            : this(null) {
        }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="errors">Errors.</param>
        public ResultCollection(IEnumerable<ErrorResult> errors) {
            bool isSuccess = true;
            bool hasWarnings = false;
            bool hasErrors = false;

            if (errors != null) {
                foreach (var error in errors) {
                    if (error.IsError) {
                        hasErrors = true;
                        isSuccess = false;
                    } else if (error.IsWarning) {
                        hasWarnings = true;
                    }
                    this.BaseCollection.Add(error);
                }
            }

            this.IsSuccess = isSuccess;
            this.HasWarnings = hasWarnings;
            this.HasErrors = hasErrors;
        }


        /// <summary>
        /// Gets if result can be considered a success.
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Gets if there are any warnings in the result.
        /// </summary>
        public bool HasWarnings { get; private set; }

        /// <summary>
        /// Gets if there are any error in the result.
        /// </summary>
        public bool HasErrors { get; private set; }


        #region ICollection

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly List<ErrorResult> BaseCollection = new List<ErrorResult>();


        /// <summary>
        /// Adds an item.
        /// Operation is not supported.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Add(ErrorResult item) {
            throw new NotSupportedException("Collection is read-only.");
        }

        /// <summary>
        /// Removes all items.
        /// Operation is not supported.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Clear() {
            throw new NotSupportedException("Collection is read-only.");
        }

        /// <summary>
        /// Determines whether the collection contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        public bool Contains(ErrorResult item) {
            if (item == null) { return false; }
            if (this.BaseCollection == null) { return false; }
            return this.BaseCollection.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(ErrorResult[] array, int arrayIndex) {
            if (this.BaseCollection == null) { return; }
            this.BaseCollection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items contained in the collection.
        /// </summary>
        public int Count {
            get { return (this.BaseCollection != null) ? this.BaseCollection.Count : 0; }
        }

        /// <summary>
        /// Searches for the specified item and returns the zero-based index of the first occurrence within the entire collection.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        public int IndexOf(ErrorResult item) {
            if (this.BaseCollection == null) { return -1; }
            return this.BaseCollection.IndexOf(item);
        }

        /// <summary>
        /// Inserts an element into the collection at the specified index.
        /// Operation is not supported.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Insert(int index, ErrorResult item) {
            throw new NotSupportedException("Collection is read-only.");
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// Value is always true.
        /// </summary>
        public bool IsReadOnly {
            get { return true; }
        }

        /// <summary>
        /// Removes the item from the collection.
        /// Operation is not supported.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public bool Remove(ErrorResult item) {
            throw new NotSupportedException("Collection is read-only.");
        }

        /// <summary>
        /// Removes the element at the specified index of the collection.
        /// Operation is not supported
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void RemoveAt(int index) {
            throw new NotSupportedException("Collection is read-only.");
        }
        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
        /// </summary>
        public IEnumerator<ErrorResult> GetEnumerator() {
            if (this.BaseCollection == null) { yield break; }
            foreach (var item in this.BaseCollection) {
                yield return item;
            }
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a non-generic collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// Set operation is not supported.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public ErrorResult this[int index] {
            get { return this.BaseCollection[index]; }
            set { throw new NotSupportedException("Collection is read-only."); }
        }

        #endregion


        #region Conversions

        /// <summary>
        /// Converts result collection into a boolean indicating success.
        /// </summary>
        /// <param name="resultCollection">Result collection.</param>
        public static Boolean ToBoolean(ResultCollection resultCollection) {
            return (resultCollection != null) ? resultCollection.IsSuccess : false;
        }

        /// <summary>
        /// Converts result collection into a boolean indicating success.
        /// </summary>
        /// <param name="resultCollection">Result collection.</param>
        public static implicit operator Boolean(ResultCollection resultCollection) {
            return ToBoolean(resultCollection);
        }

        /// <summary>
        /// Converts boolean indicating success into a result collection.
        /// </summary>
        /// <param name="isSuccess">Success flag.</param>
        public static ResultCollection FromBoolean(Boolean isSuccess) {
            return new ResultCollection(null) { IsSuccess = isSuccess };
        }

        /// <summary>
        /// Converts boolean indicating success into a result collection.
        /// </summary>
        /// <param name="isSuccess">Success flag.</param>
        public static implicit operator ResultCollection(Boolean isSuccess) {
            return FromBoolean(isSuccess);
        }


        /// <summary>
        /// Converts ErrorResult into a result collection.
        /// </summary>
        /// <param name="result">Result.</param>
        public static ResultCollection FromErrorResult(ErrorResult result) {
            return new ResultCollection(new ErrorResult[] { result });
        }

        /// <summary>
        /// Converts boolean indicating success into a result collection.
        /// </summary>
        /// <param name="result">Result.</param>
        public static implicit operator ResultCollection(ErrorResult result) {
            return FromErrorResult(result);
        }

        #endregion


        #region Clone

        /// <summary>
        /// Creates a copy of the collection.
        /// </summary>
        public ResultCollection Clone() {
            if (this.Count == 0) {
                return new ResultCollection(this.BaseCollection) { IsSuccess = this.IsSuccess };
            } else {
                return new ResultCollection(this.BaseCollection) { IsSuccess = this.IsSuccess };
            }
        }

        /// <summary>
        /// Creates a copy of the collection.
        /// </summary>
        /// <param name="newPrefix">New prefix for all text elements.</param>
        /// <exception cref="System.ArgumentNullException">New prefix cannot be null.</exception>
        public ResultCollection Clone(string newPrefix) {
            if (newPrefix == null) { throw new ArgumentNullException(nameof(newPrefix), "New prefix cannot be null."); }
            if (this.Count == 0) {
                return new ResultCollection(this.BaseCollection) { IsSuccess = this.IsSuccess };
            } else {
                var newErrors = new List<ErrorResult>();
                foreach (var error in this.BaseCollection) { 
                    newErrors.Add(error.Clone(newPrefix));
                }
                return new ResultCollection(newErrors) { IsSuccess = this.IsSuccess };
            }
        }

        #endregion

    }
}
