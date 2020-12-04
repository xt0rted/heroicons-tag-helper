namespace IconSourceGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;

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
            var source = new StringBuilder(@"
namespace Tailwind.Heroicons
{
    public enum IconSymbol
    {
");

            foreach (var icon in icons.First().Value)
            {
                source.AppendLine("        /// <summary>");
                source.Append("        /// Heroicon name: ").AppendLine(icon.Name);
                source.AppendLine("        /// </summary>");
                source.Append("        ").Append(icon.ClassName).AppendLine(",");
            }

            source.AppendLine(@"
    }
}");

            context.AddSource("IconSymbolEnum", SourceText.From(source.ToString(), Encoding.UTF8));
        }

        private void BuildIconListClass(GeneratorExecutionContext context, Dictionary<string, List<IconDetails>> icons)
        {
            var source = new StringBuilder(@"
namespace Tailwind.Heroicons
{
    using System;

    public static class IconList
    {
");

            foreach (var style in icons)
            {
                source.Append("        public static Icon ");
                source.Append(style.Key.FirstCharToUpper());
                source.Append("(IconSymbol symbol");

                if (style.Value.Any(i => i.UsesStroke))
                {
                    source.Append(", string strokeWidth = null");
                }

                source.AppendLine(@")
        {
            switch (symbol)
            {");

                foreach (var icon in style.Value)
                {
                    var file = icon.File.GetText(context.CancellationToken).ToString();
                    var path = IconExtractor.GetPaths(file);
                    var viewBox = IconExtractor.GetViewBox(file);

                    // Escape the path string before replacing the stroke-width attribute so we can more easily use format strings for it
                    path = path.Replace("\"", "\\\"");

                    if (icon.UsesStroke)
                    {
                        path = IconExtractor.ConfigureStrokeWidth(path);
                    }

                    source.Append("                case IconSymbol.").Append(icon.ClassName).AppendLine(":");
                    source.AppendLine("                    return new Icon");
                    source.AppendLine("                    {");
                    source.Append("                        Name = \"").Append(icon.Name).AppendLine("\",");
                    source.Append("                        Path = ").Append(icon.UsesStroke ? "$" : "").Append("\"").Append(path).AppendLine("\",");
                    source.Append("                        ViewBox = \"").Append(viewBox).AppendLine("\",");
                    source.AppendLine("                    };");
                    source.AppendLine("");
                }

                source.AppendLine(@"
                default:
                    throw new ArgumentOutOfRangeException(nameof(symbol), symbol, ""Unsupported icon name"");
            }
        }");
            }

            source.AppendLine(@"
    }

    public class Icon
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ViewBox { get; set; }
    }
}
");

            context.AddSource("IconListClass", SourceText.From(source.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // System.Diagnostics.Debugger.Launch();
        }
    }
}
