using System.Text.RegularExpressions;

namespace TsqltNet
{
    public class IdentifierFormatter : IIdentifierFormatter
    {
        private static readonly Regex InvalidCharactersRegex = new Regex(@"[^\w\s_-]");
        private static readonly Regex UnderscoreReplacementRegex = new Regex(@"[\s_-]+");

        public string FormatString(string inputString)
        {
            var stringWithoutInvalidCharacters = InvalidCharactersRegex.Replace(inputString, "");
            return UnderscoreReplacementRegex.Replace(stringWithoutInvalidCharacters, "_");
        }
    }
}
