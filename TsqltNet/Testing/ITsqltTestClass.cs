namespace TsqltNet.Testing
{
    public interface ITsqltTestClass
    {
        string TestClassSchemaName { get; }
        
        /// <summary>
        /// A name for this class that is safe to be assigned to a CLR type
        /// </summary>
        string NormalizedTestClassName { get; }

        ITsqltTest[] Tests { get; }

        ITsqltTest GetTestByProcedureName(string testProcedureName);
    }
}