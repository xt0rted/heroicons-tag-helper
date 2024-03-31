namespace Tailwind.Heroicons;

/// <summary>
/// Tag helper that sets the <c>aria-hidden</c> or <c>role</c> attribute based on if <c>aria-label</c> or <c>aria-labeledby</c> are set.
/// The attributes will only be set if they don't already exist on the element.
/// </summary>
[HtmlTargetElement("heroicon-micro")]
[HtmlTargetElement("heroicon-mini")]
[HtmlTargetElement("heroicon-outline")]
[HtmlTargetElement("heroicon-solid")]
public class IconAccessibilityTagHelper : TagHelper
{
    private readonly HeroiconOptions _settings;

    /// <summary>
    /// Creates a new <see cref="IconAccessibilityTagHelper"/>.
    /// </summary>
    /// <param name="settings">The <see cref="HeroiconOptions"/> to use when processing the target element.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public IconAccessibilityTagHelper(IOptions<HeroiconOptions> settings)
        => _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    /// <inheritdoc/>
    public override int Order => 1000;

    /// <inheritdoc/>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        if (!_settings.SetAccessibilityAttributes)
        {
            return;
        }

        var isUsedAsImage =
            output.Attributes.ContainsName("aria-label") ||
            output.Attributes.ContainsName("aria-labeledby");

        if (isUsedAsImage)
        {
            // If the attribute is already set then honor that value instead of forcing it to img
            if (!output.Attributes.ContainsName("role"))
            {
                output.Attributes.SetAttribute("role", "img");
            }
        }
        else
        {
            // If the attribute is already set then honor that value instead of forcing it to true
            if (!output.Attributes.ContainsName("aria-hidden"))
            {
                output.Attributes.SetAttribute("aria-hidden", "true");
            }
        }
    }
}
