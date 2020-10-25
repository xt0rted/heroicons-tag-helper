namespace IconSourceGenerator
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal static class IconExtractor
    {
        public static string GetPaths(string icon)
        {
            var lines = icon.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(path => path.Trim()).ToArray();

            var paths = lines.Skip(1).Take(lines.Length - 2);

            return string.Join("", paths);
        }

        public static (int width, int height) GetSize(string icon)
        {
            var match = Regex.Match(icon, "viewBox=\"(\\d+) (\\d+) (?<width>\\d+) (?<height>\\d+)\"", RegexOptions.Compiled);

            return
            (
                width: Convert.ToInt32(match.Groups["width"].Value),
                height: Convert.ToInt32(match.Groups["height"].Value)
            );
        }
    }
}
