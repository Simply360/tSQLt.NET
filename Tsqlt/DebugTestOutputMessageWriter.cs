using System.Diagnostics;

namespace Tsqlt
{
    public class DebugTestOutputMessageWriter : ITestOutputMessageWriter
    {
        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
    }
}