using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tsqlt
{
    public class ResourcesHelper
    {
        public string GetEmbeddedResource(Assembly assembly, string ns, string res)
        {
            var fullyQualifiedResourceName = $"{ns}.{res}";
            return GetEmbeddedResource(assembly, fullyQualifiedResourceName);
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
        
        public IEnumerable<string> GetEmbeddedResourceScriptsFrom(Assembly assembly, IEnumerable<string> fullyQualifiedResourceNames)
        {
            var resourceScripts = new List<string>();
            if (fullyQualifiedResourceNames == null) return resourceScripts;
            resourceScripts.AddRange(fullyQualifiedResourceNames.Select(n => GetEmbeddedResource(assembly, n)));

            return resourceScripts;
        }
    }
}
