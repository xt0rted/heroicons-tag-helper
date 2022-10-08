namespace Tailwind.Heroicons;

[HtmlTargetElement("heroicon-outline")]
[HtmlTargetElement("heroicon-solid")]
public class IconFocusableTagHelper : TagHelper
{
    private readonly HeroiconOptions _settings;

    public IconFocusableTagHelper(IOptions<HeroiconOptions> settings)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public override int Order => 1000;

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
