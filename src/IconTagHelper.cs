namespace Tailwind.Heroicons;

/// <summary>
/// Tag helper that emits Heroicon icons as inline svg elements.
/// </summary>
[HtmlTargetElement("heroicon-micro", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("heroicon-mini", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("heroicon-outline", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("heroicon-solid", TagStructure = TagStructure.WithoutEndTag)]
[OutputElementHint("svg")]
public class IconTagHelper : TagHelper
{
    private readonly HeroiconOptions _settings;

    /// <summary>
    /// Creates a new <see cref="IconTagHelper"/>.
    /// </summary>
    /// <param name="settings">The <see cref="HeroiconOptions"/> to use when processing the target element.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public IconTagHelper(IOptions<HeroiconOptions> settings)
        => _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    /// <inheritdoc/>
    public override int Order => 0;

    /// <summary>
    /// The name of the Heroicon to use.
    /// </summary>
    [HtmlAttributeName("icon")]
    public IconSymbol Icon { get; set; }

    /// <inheritdoc/>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        var isMicro = context.TagName.Equals("heroicon-micro", StringComparison.OrdinalIgnoreCase);
        var isMini = context.TagName.Equals("heroicon-mini", StringComparison.OrdinalIgnoreCase);
        var isSolid = context.TagName.Equals("heroicon-solid", StringComparison.OrdinalIgnoreCase);

        Icon icon;

        if (isMicro)
        {
            icon = IconList.Micro(Icon);
        }
        else if (isMini)
        {
            icon = IconList.Mini(Icon);
        }
        else if (isSolid)
        {
            icon = IconList.Solid(Icon);
        }
        else
        {
            icon = IconList.Outline(Icon);
        }

        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "svg";

        if (isMicro || isMini || isSolid)
        {
            output.Attributes.SetAttribute("fill", "currentColor");
        }
        else
        {
            output.Attributes.SetAttribute("fill", "none");
            output.Attributes.SetAttribute("stroke", "currentColor");
            output.Attributes.SetAttribute("stroke-width", icon.StrokeWidth);
        }

        // TODO: Fix this
        //output.Attributes.SetAttribute("viewbox", icon.ViewBox);

        output.Content.AppendHtml(icon.Path);

        if (_settings.IncludeComments)
        {
            var iconStyle = IconStyle(
                isMicro,
                isMini,
                isSolid);

            output.PreElement.AppendHtml("<!-- Heroicon name: ");
            output.PreElement.Append(iconStyle);
            output.PreElement.Append(" ");
            output.PreElement.Append(icon.Name);
            output.PreElement.AppendHtmlLine(" -->");
        }
    }

    private static string IconStyle(
        bool isMicro,
        bool isMini,
        bool isSolid)
    {
        if (isMicro)
        {
            return "micro";
        }

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
