namespace Tailwind.Heroicons
{
    public class IconFocusableTagHelperTests : TagHelperTestBase
    {
        [Fact]
        public void Should_not_set_focusable_attribute_when_disabled()
        {
            // Given
            var context = MakeTagHelperContext("heroicon-outline");
            var output = MakeTagHelperOutput("heroicon-outline");

            var options = Options.Create(new HeroiconOptions { SetFocusableAttribute = false });
            var helper = new IconFocusableTagHelper(options);

            // When
            helper.Process(context, output);

            // Then
            output.Attributes.ShouldNotContain(a => a.Name == "focusable");
        }

        [Fact]
        public void Should_set_focusable_attribute()
        {
            // Given
            var context = MakeTagHelperContext("heroicon-outline");
            var output = MakeTagHelperOutput("heroicon-outline");

            var options = Options.Create(new HeroiconOptions { SetFocusableAttribute = true });
            var helper = new IconFocusableTagHelper(options);

            // When
            helper.Process(context, output);

            // Then
            AssertAttributeValue(output.Attributes, "focusable", "false");
        }

        [Fact]
        public void Should_set_focusable_attribute_to_false()
        {
            // Given
            var context = MakeTagHelperContext(
                "heroicon-outline",
                new TagHelperAttributeList
                {
                    { "focusable", "true" },
                });
            var output = MakeTagHelperOutput(
                "heroicon-outline",
                new TagHelperAttributeList
                {
                    { "focusable", "true" },
                });

            var options = Options.Create(new HeroiconOptions { SetFocusableAttribute = true });
            var helper = new IconFocusableTagHelper(options);

            // When
            helper.Process(context, output);

            // Then
            AssertAttributeValue(output.Attributes, "focusable", "false");
        }
    }
}
