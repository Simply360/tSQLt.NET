namespace TsqltNet.Testing
{
    public interface ITsqltTest
    {
        string ProcedureName { get; }

        string NormalizedTestMethodName { get; }

        string TestCaseBody { get; }
    }
}