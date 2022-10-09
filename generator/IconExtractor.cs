namespace IconSourceGenerator;

internal static class IconExtractor
{
    private static readonly Regex ViewBoxRegEx = new("viewBox=\"(?<viewbox>[^\"]+)\"", RegexOptions.Compiled);
    private static readonly Regex StrokeWidthRegEx = new("stroke-width=\"(?<width>[^\"]+)\"", RegexOptions.Compiled);

    public static string GetPaths(string icon)
    {
        var lines = icon.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(path => path.Trim()).ToArray();

        var paths = lines.Skip(1).Take(lines.Length - 2);

        return string.Concat(paths);
    }

    public static string GetViewBox(string icon)
    {
        var match = ViewBoxRegEx.Match(icon);

        return match.Groups["viewbox"].Value;
    }

    public static string GetStrokeWidth(string icon)
    {
        var match = StrokeWidthRegEx.Match(icon);

        return match.Groups["width"].Value;
    }
}
