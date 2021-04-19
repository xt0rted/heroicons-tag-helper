namespace Tailwind.Heroicons
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    internal static class TagHelperAttributeExtensions
    {
        public static string ToStringValue(this TagHelperAttribute attribute) =>
            attribute.Value switch
            {
                HtmlString htmlString => htmlString.ToString(),
                string stringValue => stringValue,
                _ => null,
            };
    }
}
