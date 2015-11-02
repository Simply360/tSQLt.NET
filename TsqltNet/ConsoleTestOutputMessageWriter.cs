using System;

namespace TsqltNet
{
    public class ConsoleTestOutputMessageWriter : ITestOutputMessageWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}