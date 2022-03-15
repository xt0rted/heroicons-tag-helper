namespace IconSourceGenerator
{
    internal static class IconExtractor
    {
        public static string GetPaths(string icon)
        {
            var lines = icon.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(path => path.Trim()).ToArray();

            var paths = lines.Skip(1).Take(lines.Length - 2);

            return string.Join("", paths);
        }

        public static string GetViewBox(string icon)
        {
            var match = Regex.Match(icon, "viewBox=\"(?<viewbox>[^\"]+)\"", RegexOptions.Compiled);

            return match.Groups["viewbox"].Value;
        }

        public static string GetStrokeWidth(string icon)
        {
            var match = Regex.Match(icon, "stroke-width=\"(?<width>[^\"]+)\"", RegexOptions.Compiled);

            return match.Groups["width"].Value;
        }
    }
}
