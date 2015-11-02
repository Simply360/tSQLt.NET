using System.Reflection;

namespace TsqltNet
{
    public interface IEmbeddedTextResourceReader
    {
        string GetResourceContents(Assembly assembly, string resourcePath);
    }
}