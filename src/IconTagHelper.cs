namespace Tailwind.Heroicons
{
    using System;

    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Options;

    [HtmlTargetElement("heroicon-outline", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("heroicon-solid", TagStructure = TagStructure.WithoutEndTag)]
    public class IconTagHelper : TagHelper
    {
        private readonly HeroiconOptions _settings;

        public IconTagHelper(IOptions<HeroiconOptions> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        [HtmlAttributeName("icon")]
        public IconSymbol Icon { get; set; }

        [HtmlAttributeName("stroke-width")]
        public string StrokeWidth { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (output is null) throw new ArgumentNullException(nameof(output));

            var isSolid = context.TagName.Equals("heroicon-solid", StringComparison.OrdinalIgnoreCase);

            var icon = isSolid
                ? IconList.Solid(Icon)
                : IconList.Outline(Icon, StrokeWidth);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "svg";

            output.Attributes.Add("aria-hidden", "true");

            if (isSolid)
            {
                output.Attributes.SetAttribute("fill", "currentColor");
            }
            else
            {
                output.Attributes.SetAttribute("fill", "none");
                output.Attributes.SetAttribute("stroke", "currentColor");
            }

            output.Attributes.SetAttribute("viewbox", icon.ViewBox);

            output.Content.AppendHtml(icon.Path);

            if (_settings.IncludeComments)
            {
                output.PreElement.AppendHtml("<!-- Heroicon name: ");
                output.PreElement.AppendHtml(isSolid ? "solid" : "outline");
                output.PreElement.AppendHtml(" ");
                output.PreElement.Append(icon.Name);
                output.PreElement.AppendHtml(" -->");
            }
        }
    }
}
