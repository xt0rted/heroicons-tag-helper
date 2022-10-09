namespace IconSourceGenerator;

[Generator]
public class IconSourceGenerator : ISourceGenerator
{
    private static readonly DiagnosticDescriptor _errorDescriptor = new DiagnosticDescriptor(
#pragma warning disable RS2008 // Enable analyzer release tracking
        "SI0000",
#pragma warning restore RS2008 // Enable analyzer release tracking
        "Error in the IconSourceGenerator generator",
        "Error in the IconSourceGenerator generator: '{0}'",
        "IconSourceGenerator",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public void Execute(GeneratorExecutionContext context)
    {
        try
        {
            ExecuteInternal(context);
        }
        catch (Exception ex)
        {
            context.ReportDiagnostic(Diagnostic.Create(_errorDescriptor, Location.None, ex.ToString()));
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ExecuteInternal(GeneratorExecutionContext context)
    {
        var icons = LoadIcons(context);

        BuildSymbolEnum(context, icons);
        BuildIconListClass(context, icons);
    }

    private Dictionary<string, List<IconDetails>> LoadIcons(GeneratorExecutionContext context) =>
        context
            .AdditionalFiles
            .Where(at => at.Path.EndsWith(".svg", StringComparison.InvariantCultureIgnoreCase))
            .Select(file =>
            {
                var options = context.AnalyzerConfigOptions.GetOptions(file);
                if (!options.TryGetValue("build_metadata.AdditionalFiles.IconStyle", out var iconStyle))
                {
                    throw new Exception($"IconStyle not specified for file {file.Path}");
                }

                context.AnalyzerConfigOptions.GetOptions(file).TryGetValue("build_metadata.AdditionalFiles.UsesStroke", out var usesStrokeValue);
                bool.TryParse(usesStrokeValue, out var usesStroke);

                var directory = Path.GetDirectoryName(file.Path);
                var name = Path.GetFileNameWithoutExtension(file.Path);

                return new IconDetails
                {
                    ClassName = name.ToPascalCase(),
                    File = file,
                    Name = name,
                    Path = file.Path,
                    Style = iconStyle,
                    UsesStroke = usesStroke,
                };
            })
            .OrderBy(o => o.Style).ThenBy(o => o.Name)
            .GroupBy(g => g.Style)
            .ToDictionary(k => k.Key, k => k.ToList());

    private void BuildSymbolEnum(GeneratorExecutionContext context, Dictionary<string, List<IconDetails>> icons)
    {
        var source = new StringBuilder(
            """
            namespace Tailwind.Heroicons
            {
                /// <summary>
                /// The available icons.
                /// </summary>
                public enum IconSymbol
                {

            """);

        foreach (var icon in icons.First().Value)
        {
            source.AppendLine("        /// <summary>");
            source.Append("        /// Heroicon name: ").AppendLine(icon.Name);
            source.AppendLine("        /// </summary>");
            source.Append("        ").Append(icon.ClassName).AppendLine(",");
        }

        source.AppendLine(
            """
                }
            }
            """);

        context.AddSource("IconSymbolEnum", SourceText.From(source.ToString(), Encoding.UTF8));
    }

    private void BuildIconListClass(GeneratorExecutionContext context, Dictionary<string, List<IconDetails>> icons)
    {
        var source = new StringBuilder(
            """
            namespace Tailwind.Heroicons
            {
                using System;

                /// <summary>
                /// Helper used to get parsed icon details.
                /// </summary>
                public static class IconList
                {

            """);

        foreach (var style in icons)
        {
            source.AppendLine(
                $$"""
                        /// <summary>
                        /// Get the details of an icon in the {{style.Key.FirstCharToUpper()}} variation.
                        /// </summary>
                        /// <param name="symbol">The <see cref="IconSymbol"/> to get the details of.</param>
                        /// <returns>The icon details.</returns>
                        public static Icon {{style.Key.FirstCharToUpper()}}(IconSymbol symbol)
                        {
                            switch (symbol)
                            {
                """);

            foreach (var icon in style.Value)
            {
                var file = icon.File.GetText(context.CancellationToken).ToString();
                var path = IconExtractor.GetPaths(file);
                var viewBox = IconExtractor.GetViewBox(file);

                // Escape the path string before writing it out
                path = path.Replace("\"", "\\\"");

                source.Append("                case IconSymbol.").Append(icon.ClassName).AppendLine(":");
                source.AppendLine("                    return new Icon");
                source.AppendLine("                    {");
                source.Append("                        Name = \"").Append(icon.Name).AppendLine("\",");
                source.Append("                        Path = \"").Append(path).AppendLine("\",");
                source.Append("                        ViewBox = \"").Append(viewBox).AppendLine("\",");

                if (icon.UsesStroke)
                {
                    var strokeWidth = IconExtractor.GetStrokeWidth(file);

                    source.Append("                        StrokeWidth = \"").Append(strokeWidth).AppendLine("\",");
                }

                source.AppendLine("                    };");
                source.AppendLine();
            }

            source.AppendLine(
                """
                                default:
                                    throw new ArgumentOutOfRangeException(nameof(symbol), symbol, "Unsupported icon name");
                            }
                        }
                """);
        }

        source.AppendLine(
            """
                }

                /// <summary>
                /// A parsed Heroicon.
                /// </summary>
                public class Icon
                {
                    /// <summary>
                    /// The filename without extension.
                    /// </summary>
                    public string Name { get; set; }

                    /// <summary>
                    /// The svg path element.
                    /// </summary>
                    public string Path { get; set; }

                    /// <summary>
                    /// The svg <c>viewbox</c> attribute value.
                    /// </summary>
                    public string ViewBox { get; set; }

                    /// <summary>
                    /// The svg <c>stroke-width</c> attribute value.
                    /// </summary>
                    public string StrokeWidth { get; set; }
                }
            }
            """);

        context.AddSource("IconListClass", SourceText.From(source.ToString(), Encoding.UTF8));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // System.Diagnostics.Debugger.Launch();
    }
}
