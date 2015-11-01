using System.Linq;
using System.Text.RegularExpressions;

namespace Tsqlt
{
    public class SqlBatchExtractor : ISqlBatchExtractor
    {
        public string[] ExtractBatches(string source)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                source,
                @"^\s*GO\s* ($ | \-\- .*$)",
                RegexOptions.Multiline |
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'))
                .ToArray();
        }
    }
}