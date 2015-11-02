namespace TsqltNet
{
    public interface ITsqltTest
    {
        string ProcedureName { get; }

        string NormalizedTestMethodName { get; }

        string TestCaseBody { get; }
    }
}