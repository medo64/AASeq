namespace WiresharkDictImport;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

internal class XmlResourceResolver : XmlResolver {

    public XmlResourceResolver(string resourceNamePrefix) {
        this.ResourceNamePrefix = resourceNamePrefix;
    }

    public string ResourceNamePrefix { get; private set; }

    public override System.Net.ICredentials Credentials {
        set {  }
    }

    public override object GetEntity(Uri absoluteUri, string? role, Type? ofObjectToReturn) {
        if (ofObjectToReturn is null) { throw new ArgumentNullException(nameof(ofObjectToReturn)); }
        if (ofObjectToReturn.Equals(typeof(Stream))) {
            var fileName = absoluteUri.Segments[absoluteUri.Segments.Length - 1];
            var resourceName = this.ResourceNamePrefix + "." + fileName;
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            return resourceStream ?? throw new FileNotFoundException($"Resource '{resourceName}' not found.");
        }
        throw new NotSupportedException();
    }

}
