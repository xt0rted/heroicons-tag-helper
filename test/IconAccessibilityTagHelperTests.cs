namespace Tailwind.Heroicons;

public class IconAccessibilityTagHelperTests : TagHelperTestBase
{
    [Fact]
    public void Should_not_set_accessibility_attributes_when_option_is_disabled()
    {
        // Given
        var context = MakeTagHelperContext("heroicon-outline");
        var output = MakeTagHelperOutput("heroicon-outline");

        var options = Options.Create(new HeroiconOptions { SetAccessibilityAttributes = false });
        var helper = new IconAccessibilityTagHelper(options);

        // When
        helper.Process(context, output);

        // Then
        output.Attributes.ShouldNotContain(a => a.Name == "aria-hidden");
        output.Attributes.ShouldNotContain(a => a.Name == "role");
    }

    [Theory]
    [InlineData("aria-label")]
    [InlineData("aria-labeledby")]
    public void Should_set_role_attribute_when_label_attribute_is_set_and_role_is_not(string attributeName)
    {
        // Given
        var context = MakeTagHelperContext(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { attributeName, "test" },
            });
        var output = MakeTagHelperOutput(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { attributeName, "test" },
            });

        var options = Options.Create(new HeroiconOptions { SetAccessibilityAttributes = true });
        var helper = new IconAccessibilityTagHelper(options);

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "role", "img");
    }

    [Theory]
    [InlineData("aria-label")]
    [InlineData("aria-labeledby")]
    public void Should_not_set_role_attribute_when_label_attribute_is_set_and_role_already_is(string attributeName)
    {
        // Given
        var context = MakeTagHelperContext(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { attributeName, "test" },
                { "role", "button" },
            });
        var output = MakeTagHelperOutput(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { attributeName, "test" },
                { "role", "button" },
            });

        var options = Options.Create(new HeroiconOptions { SetAccessibilityAttributes = true });
        var helper = new IconAccessibilityTagHelper(options);

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "role", "button");
    }

    [Fact]
    public void Should_set_aria_hidden_attribute_to_true_when_not_used_as_an_image_and_attribute_is_not_already_set()
    {
        // Given
        var context = MakeTagHelperContext("heroicon-outline");
        var output = MakeTagHelperOutput("heroicon-outline");

        var options = Options.Create(new HeroiconOptions { SetAccessibilityAttributes = true });
        var helper = new IconAccessibilityTagHelper(options);

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "aria-hidden", "true");
    }

    [Fact]
    public void Should_not_set_aria_hidden_attribute_to_true_when_not_used_as_an_image_and_attribute_is_already_set()
    {
        // Given
        var context = MakeTagHelperContext(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { "aria-hidden", "foo" },
            });
        var output = MakeTagHelperOutput(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { "aria-hidden", "foo" },
            });

        var options = Options.Create(new HeroiconOptions { SetAccessibilityAttributes = true });
        var helper = new IconAccessibilityTagHelper(options);

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "aria-hidden", "foo");
    }
}
