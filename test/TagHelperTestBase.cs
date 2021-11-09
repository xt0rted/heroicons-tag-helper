namespace Tailwind.Heroicons
{
    public abstract class TagHelperTestBase
    {
        protected static TagHelperContext MakeTagHelperContext(string tagName, TagHelperAttributeList attributes = null)
        {
            attributes ??= new TagHelperAttributeList();

            return new TagHelperContext(
                tagName,
                allAttributes: attributes,
                items: new Dictionary<object, object>(),
                uniqueId: Guid.NewGuid().ToString("N"));
        }

        protected static TagHelperOutput MakeTagHelperOutput(string tagName, TagHelperAttributeList attributes = null)
        {
            attributes ??= new TagHelperAttributeList();

            return new TagHelperOutput(
                tagName,
                attributes: attributes,
                getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
        }

        protected static void AssertAttributeValue(TagHelperAttributeList attributes, string name, string value)
        {
            attributes
                .Count(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ShouldBe(1);

            attributes[name].Value.ShouldBe(value);
        }
    }
}
