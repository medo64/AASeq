namespace AASeq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

[DebuggerDisplay("{Count} {Count == 1 ? \"tag\" : \"tags\",nq}")]
public sealed class TagCollection
    : IReadOnlyList<Tag>
    , IEquatable<TagCollection>
    , IEqualityOperators<TagCollection, TagCollection, bool> {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="items">Items in the collection.</param>
    /// <exception cref="ArgumentNullException">Items cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Duplicate name in collection.</exception>
    public TagCollection(IEnumerable<Tag> items) {
        Helpers.ThrowIfArgumentNull(items, nameof(items), "Items cannot be null.");
        foreach (var item in items) {
            Helpers.ThrowIfArgumentFalse(LookupDict.TryAdd(item.Name, item),
                nameof(items), "Duplicate name in collection.");
        }
        BaseCollection.AddRange(items);
    }


    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly List<Tag> BaseCollection = new();
    private readonly Dictionary<string, Tag> LookupDict = new(Tag.NameComparer);


    /// <summary>
    /// Returns true if the normal tag is present and in enabled (true) state.
    /// </summary>
    /// <param name="tagName">Tag name.</param>
    public bool IsTagEnabled(string tagName) {
        if (LookupDict.TryGetValue(tagName, out var tag)) {
            if (tag.State && !tag.IsSystem && Tag.NameComparer.Equals(tagName, tag.Name)) { return true; }
        }
        return false;
    }

    /// <summary>
    /// Returns true if the normal tag is present and in disabled (false) state.
    /// </summary>
    public bool IsTagDisabled(string tagName) {
        if (LookupDict.TryGetValue(tagName, out var tag)) {
            if (!tag.State && !tag.IsSystem && Tag.NameComparer.Equals(tagName, tag.Name)) { return true; }
        }
        return false;
    }


    /// <summary>
    /// Returns true if the system tag is present and in enabled (true) state.
    /// </summary>
    /// <param name="tagName">Tag name.</param>
    public bool IsSystemTagEnabled(string tagName) {
        if (LookupDict.TryGetValue(tagName, out var tag)) {
            if (tag.State && tag.IsSystem && Tag.NameComparer.Equals(tagName, tag.Name)) { return true; }
        }
        return false;
    }

    /// <summary>
    /// Returns true if the system tag is present and in disabled (false) state.
    /// </summary>
    public bool IsSystemTagDisabled(string tagName) {
        if (LookupDict.TryGetValue(tagName, out var tag)) {
            if (!tag.State && tag.IsSystem && Tag.NameComparer.Equals(tagName, tag.Name)) { return true; }
        }
        return false;
    }


    /// <summary>
    /// Enumerates over all normal tags.
    /// </summary>
    public IEnumerable<Tag> EnumerateTags() {
        foreach (var item in BaseCollection) {
            if (!item.IsSystem) { yield return item; }
        }
    }

    /// <summary>
    /// Enumerates over all system tags.
    /// </summary>
    public IEnumerable<Tag> EnumerateSystemTags() {
        foreach (var item in BaseCollection) {
            if (item.IsSystem) { yield return item; }
        }
    }


    #region IReadOnlyList<Tag>

    /// <inheritdoc/>
    public Tag this[int index] => BaseCollection[index];

    /// <inheritdoc/>
    public int Count => BaseCollection.Count;

    /// <inheritdoc/>
    public IEnumerator<Tag> GetEnumerator() {
        return BaseCollection.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() {
        return BaseCollection.GetEnumerator();
    }

    #endregion IReadOnlyList<Tag>


    #region IEquatable<TagCollection>

    public bool Equals(TagCollection? other) {
        if (other is null) { return false; }
        if (Count != other.Count) { return false; }

        using var enum1 = GetEnumerator();
        using var enum2 = other.GetEnumerator();
        while (enum1.MoveNext() && enum2.MoveNext()) {
            if (enum1.Current != enum2.Current) {
                return false;
            }
        }
        return true;
    }

    #endregion IEquatable<TagCollection>


    #region IEqualityOperators<TagCollection, TagCollection, bool>

    /// <inheritdoc/>
    public static bool operator ==(TagCollection? obj1, TagCollection? obj2) {
        return (obj1 is not null) && obj1.Equals(obj2);
    }

    /// <inheritdoc/>
    public static bool operator !=(TagCollection? obj1, TagCollection? obj2) {
        return !(obj1 == obj2);
    }

    #endregion IEqualityOperators<TagCollection, TagCollection, bool>


    #region Overrides

    /// <inheritdoc/>
    public override bool Equals(object? obj) {
        return (obj is TagCollection other) && Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode() {
        return Count;
    }

    #endregion Overrides

}
