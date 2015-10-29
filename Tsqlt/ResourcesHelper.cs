using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tsqlt
{
    public class ResourcesHelper
    {
        public string GetEmbeddedResource(string ns, string res)
        {
            var fullyQualifiedResourceName = $"{ns}.{res}";
            return GetEmbeddedResource(fullyQualifiedResourceName);
        }

        private string GetEmbeddedResource(string fullyQualifiedResourceName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var stream = executingAssembly.GetManifestResourceStream(fullyQualifiedResourceName);
            if (stream == null) throw new Exception($"Could not located the resource at {fullyQualifiedResourceName}");
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public IEnumerable<string> GetAllEmbeddedResourcesInAssembly()
        {
            return GetType().Assembly.GetManifestResourceNames();
        }

        public IEnumerable<string> GetEmbeddedResourceScriptsFrom(IEnumerable<string> fullyQualifiedResourceNames)
        {
            var resourceScripts = new List<string>();
            if (fullyQualifiedResourceNames == null) return resourceScripts;
            resourceScripts.AddRange(fullyQualifiedResourceNames.Select(GetEmbeddedResource));

            return resourceScripts;
        }
    }
}
