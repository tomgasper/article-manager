using System;
using System.Linq;
using System.Text;

namespace ArticleManager.Utils
{
    public static class MarkdownUtils
    {
        public static string NormalizeWhiteSpaceText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var lines = GetLines(text);
            if (lines.Length == 0)
                return text;

            // Find the first non-empty line and determine the number of leading spaces to trim
            string firstNonEmptyLine = lines.FirstOrDefault(line => !string.IsNullOrWhiteSpace(line));
            if (firstNonEmptyLine == null)
                return text;

            int leadingWhitespaceCount = firstNonEmptyLine.TakeWhile(char.IsWhiteSpace).Count();

            StringBuilder sb = new StringBuilder();
            foreach (var line in lines)
            {
                leadingWhitespaceCount = line.TakeWhile(char.IsWhiteSpace).Count();
                // Trim the calculated number of leading spaces from each line
                if (line.Length >= leadingWhitespaceCount)
                    sb.AppendLine(line.Substring(leadingWhitespaceCount));
                else
                    sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public static string[] GetLines(string s, int maxLines = 0)
        {
            if (s == null)
                return Array.Empty<string>();

            s = s.Replace("\r\n", "\n");

            if (maxLines > 0)
                return s.Split('\n').Take(maxLines).ToArray();

            return s.Split('\n');
        }
    }
}