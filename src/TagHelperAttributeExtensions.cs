namespace Tailwind.Heroicons;

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
