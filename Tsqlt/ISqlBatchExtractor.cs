namespace Tsqlt
{
    /// <summary>
    /// Service for extracting batches of SQL statements from a string. The delimiter
    /// for the statements is a blank line consisting of "GO".
    /// </summary>
    public interface ISqlBatchExtractor
    {
        string[] ExtractBatches(string source);
    }
}