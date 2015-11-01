using System.Reflection;

namespace Tsqlt
{
    public interface IEmbeddedTextResourceReader
    {
        string GetResourceContents(Assembly assembly, string resourcePath);
    }
}