namespace Tailwind.Heroicons;

[HtmlTargetElement("heroicon-outline")]
[HtmlTargetElement("heroicon-solid")]
public class IconAccessibilityTagHelper : TagHelper
{
    private readonly HeroiconOptions _settings;

    public IconAccessibilityTagHelper(IOptions<HeroiconOptions> settings)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public override int Order => 1000;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));
        if (output is null) throw new ArgumentNullException(nameof(output));

        if (!_settings.SetAccessibilityAttributes)
        {
            return;
        }

        var ariaLabel = output.Attributes["aria-label"]?.ToStringValue();
        var ariaLabeledBy = output.Attributes["aria-labeledby"]?.ToStringValue();

        var isUsedAsImage =
            !string.IsNullOrWhiteSpace(ariaLabel) ||
            !string.IsNullOrWhiteSpace(ariaLabeledBy);

        if (isUsedAsImage)
        {
            output.Attributes.SetAttribute("role", "img");
        }
        else
        {
            output.Attributes.SetAttribute("aria-hidden", "true");
        }
    }
}
