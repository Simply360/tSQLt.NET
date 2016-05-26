namespace TsqltNet
{
    public class InstalledTest : IInstalledTest
    {
        public InstalledTest(string fullTestName)
        {
            FullTestName = fullTestName;
        }

        public string FullTestName { get; }
    }
}