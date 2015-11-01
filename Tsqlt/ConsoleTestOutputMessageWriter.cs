using System;

namespace Tsqlt
{
    public class ConsoleTestOutputMessageWriter : ITestOutputMessageWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}