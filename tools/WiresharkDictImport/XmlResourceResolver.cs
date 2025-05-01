namespace WiresharkDictImport;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

internal class XmlResourceResolver : XmlResolver {

    public XmlResourceResolver(string dir) {
        Dir = dir;
    }

    public string Dir { get; }

    public override System.Net.ICredentials Credentials {
        set { }
    }

    public override object GetEntity(Uri absoluteUri, string? role, Type? ofObjectToReturn) {
        if (ofObjectToReturn is null) { throw new ArgumentNullException(nameof(ofObjectToReturn)); }
        if (ofObjectToReturn.Equals(typeof(Stream))) {
            var fileName = absoluteUri.Segments[absoluteUri.Segments.Length - 1];
            var entityFileName = Path.Combine(Dir, fileName);
            return File.OpenRead(entityFileName);
        }
        throw new NotSupportedException();
    }

}
