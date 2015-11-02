using System.Diagnostics;

namespace TsqltNet
{
    public class DebugTestOutputMessageWriter : ITestOutputMessageWriter
    {
        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
    }
}