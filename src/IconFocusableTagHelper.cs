namespace Tailwind.Heroicons;

/// <summary>
/// Tag helper that sets <c>focusable</c> to <c>false</c> on <see cref="IconTagHelper"/> instances.
/// </summary>
[HtmlTargetElement("heroicon-mini")]
[HtmlTargetElement("heroicon-outline")]
[HtmlTargetElement("heroicon-solid")]
public class IconFocusableTagHelper : TagHelper
{
    private readonly HeroiconOptions _settings;

    /// <summary>
    /// Creates a new <see cref="IconFocusableTagHelper"/>.
    /// </summary>
    /// <param name="settings">The <see cref="HeroiconOptions"/> to use when processing the target element.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public IconFocusableTagHelper(IOptions<HeroiconOptions> settings)
        => _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    /// <inheritdoc/>
    public override int Order => 1000;

    /// <inheritdoc/>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));
        if (output is null) throw new ArgumentNullException(nameof(output));

        if (!_settings.SetFocusableAttribute)
        {
            return;
        }

        output.Attributes.SetAttribute("focusable", "false");
    }
}
