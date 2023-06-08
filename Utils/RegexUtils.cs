using System.Text.RegularExpressions;

namespace KwiatkiBeatkiAPI.Utils
{
    public static class RegexUtils
    {
        public enum RegexTypes : short
        {
            CodeFromSqlExMessage = 2601
        }
        public static readonly Dictionary<int, Patterns> Patterns = new Dictionary<int, Patterns>() 
        {
            { 2601, new Patterns(CodePattern: "(?<=')[a-zA-Z0-9_]+(?=')", ValuePattern: @"(?<=\()[^\)]+(?=\))") }
        };
        public static string GetValueByPattern(string? pattern, string searchText)
        {
            if (pattern == null) return "";

            var regex = new Regex(pattern)
                .Match(searchText)
                .Value;

            return regex;
        }
    }

    public record Patterns(string CodePattern, string ValuePattern);
}
