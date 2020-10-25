namespace Tailwind.Heroicons
{
    using System;

    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("heroicon-outline", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("heroicon-solid", TagStructure = TagStructure.WithoutEndTag)]
    public class IconTagHelper : TagHelper
    {
        [HtmlAttributeName("icon")]
        public IconSymbol Icon { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var isSolid = context.TagName.Equals("heroicon-solid", StringComparison.OrdinalIgnoreCase);

            var icon = isSolid
                ? IconList.Solid(Icon)
                : IconList.Outline(Icon);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "svg";

            output.Attributes.Add("aria-hidden", "true");

            if (isSolid)
            {
                output.Attributes.Add("fill", "currentColor");
            }
            else
            {
                output.Attributes.Add("fill", "none");
                output.Attributes.Add("stroke", "currentColor");
            }

            output.Attributes.Add("viewbox", icon.ViewBox);

            output.Content.AppendHtml(icon.Path);
        }
    }
}
