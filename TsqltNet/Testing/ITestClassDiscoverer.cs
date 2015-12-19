namespace TsqltNet.Testing
{
    public interface ITestClassDiscoverer
    {
        ITsqltTestClass[] DiscoverTests();
    }
}