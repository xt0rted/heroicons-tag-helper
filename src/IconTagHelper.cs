namespace Tailwind.Heroicons;


[HtmlTargetElement("heroicon-mini", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("heroicon-outline", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("heroicon-solid", TagStructure = TagStructure.WithoutEndTag)]
public class IconTagHelper : TagHelper
{
    private readonly HeroiconOptions _settings;

    public IconTagHelper(IOptions<HeroiconOptions> settings)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public override int Order => 0;

    [HtmlAttributeName("icon")]
    public IconSymbol Icon { get; set; }

    [HtmlAttributeName("stroke-width")]
    public string StrokeWidth { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));
        if (output is null) throw new ArgumentNullException(nameof(output));

        var isMini = context.TagName.Equals("heroicon-mini", StringComparison.OrdinalIgnoreCase);
        var isSolid = context.TagName.Equals("heroicon-solid", StringComparison.OrdinalIgnoreCase);

        var icon = isMini
            ? IconList.Mini(Icon)
            : isSolid
                ? IconList.Solid(Icon)
                : IconList.Outline(Icon);

        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "svg";

        if (isMini || isSolid)
        {
            output.Attributes.SetAttribute("fill", "currentColor");
        }
        else
        {
            output.Attributes.SetAttribute("fill", "none");
            output.Attributes.SetAttribute("stroke", "currentColor");
            output.Attributes.SetAttribute("stroke-width", StrokeWidth ?? icon.StrokeWidth);
        }

        output.Attributes.SetAttribute("viewbox", icon.ViewBox);

        output.Content.AppendHtml(icon.Path);

        if (_settings.IncludeComments)
        {
            output.PreElement.AppendHtml("<!-- Heroicon name: ");
            output.PreElement.Append(IconStyle(isMini, isSolid));
            output.PreElement.Append(" ");
            output.PreElement.Append(icon.Name);
            output.PreElement.AppendHtmlLine(" -->");
        }
    }

    private static string IconStyle(bool isMini, bool isSolid)
    {
        if (isMini)
        {
            return "mini";
        }

        if (isSolid)
        {
            return "solid";
        }

        return "outline";
    }
}
