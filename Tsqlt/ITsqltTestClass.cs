namespace Tsqlt
{
    public interface ITsqltTestClass
    {
        string Name { get; }

        ITsqltTest[] Tests { get; }
    }
}