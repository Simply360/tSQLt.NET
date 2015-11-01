using System;
using System.IO;
using System.Reflection;

namespace Tsqlt
{
    public class EmbeddedTextResourceReader : IEmbeddedTextResourceReader
    {
        public string GetResourceContents(Assembly assembly, string resourcePath)
        {
            return GetEmbeddedResource(assembly, resourcePath);
        }

        private string GetEmbeddedResource(Assembly assembly, string fullyQualifiedResourceName)
        {
            var stream = assembly.GetManifestResourceStream(fullyQualifiedResourceName);
            if (stream == null) throw new Exception($"Could not locate the resource at {fullyQualifiedResourceName}");
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}