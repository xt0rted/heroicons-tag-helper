namespace Tailwind.Heroicons;

public class IconTagHelperTests : TagHelperTestBase
{
    [Fact]
    public void Should_set_svg_attributes()
    {
        // Given
        var context = MakeTagHelperContext("heroicon-outline");
        var output = MakeTagHelperOutput("heroicon-outline");

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options);

        // When
        helper.Process(context, output);

        // Then
        output.TagMode.ShouldBe(TagMode.StartTagAndEndTag);
        output.TagName.ShouldBe("svg");
        AssertAttributeValue(output.Attributes, "viewbox", "0 0 24 24");
    }

    [Fact]
    public void Should_overwrite_existing_attributes_for_solid_style()
    {
        // Given
        var context = MakeTagHelperContext(
            "heroicon-solid",
            new TagHelperAttributeList
            {
                { "fill", "blue" },
                { "viewbox", "0 0 1 1" },
            });
        var output = MakeTagHelperOutput(
            "heroicon-solid",
            new TagHelperAttributeList
            {
                { "fill", "blue" },
                { "viewbox", "0 0 1 1" },
            });

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "fill", "currentColor");
        AssertAttributeValue(output.Attributes, "viewbox", "0 0 24 24");
        output.Attributes.ShouldNotContain(a => a.Name == "stroke");
    }

    [Fact]
    public void Should_overwrite_existing_attributes_for_outline_style()
    {
        // Given
        var context = MakeTagHelperContext(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { "fill", "blue" },
                { "stroke", "blue" },
                { "viewbox", "0 0 1 1" },
            });
        var output = MakeTagHelperOutput(
            "heroicon-outline",
            new TagHelperAttributeList
            {
                { "fill", "blue" },
                { "stroke", "blue" },
                { "viewbox", "0 0 1 1" },
            });

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "fill", "none");
        AssertAttributeValue(output.Attributes, "stroke", "currentColor");
        AssertAttributeValue(output.Attributes, "viewbox", "0 0 24 24");
    }

    [Theory]
    [InlineData("heroicon-micro")]
    [InlineData("Heroicon-Micro")]
    [InlineData("HEROICON-MICRO")]
    public void Should_output_micro_bell_icon(string tagName)
    {
        // Given
        var context = MakeTagHelperContext(tagName);
        var output = MakeTagHelperOutput(tagName);

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "fill", "currentColor");
        AssertAttributeValue(output.Attributes, "viewbox", "0 0 16 16");
        output.Attributes.ShouldNotContain(a => a.Name == "stroke");
        output.Attributes.ShouldNotContain(a => a.Name == "stroke-width");
        output.Content.GetContent().ShouldBe(
            """
            <path fill-rule="evenodd" d="M12 5a4 4 0 0 0-8 0v2.379a1.5 1.5 0 0 1-.44 1.06L2.294 9.707a1 1 0 0 0-.293.707V11a1 1 0 0 0 1 1h2a3 3 0 1 0 6 0h2a1 1 0 0 0 1-1v-.586a1 1 0 0 0-.293-.707L12.44 8.44A1.5 1.5 0 0 1 12 7.38V5Zm-5.5 7a1.5 1.5 0 0 0 3 0h-3Z" clip-rule="evenodd"/>
            """);
    }

    [Theory]
    [InlineData("heroicon-mini")]
    [InlineData("Heroicon-Mini")]
    [InlineData("HEROICON-MINI")]
    public void Should_output_mini_bell_icon(string tagName)
    {
        // Given
        var context = MakeTagHelperContext(tagName);
        var output = MakeTagHelperOutput(tagName);

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "fill", "currentColor");
        AssertAttributeValue(output.Attributes, "viewbox", "0 0 20 20");
        output.Attributes.ShouldNotContain(a => a.Name == "stroke");
        output.Attributes.ShouldNotContain(a => a.Name == "stroke-width");
        output.Content.GetContent().ShouldBe(
            """
            <path fill-rule="evenodd" d="M10 2a6 6 0 0 0-6 6c0 1.887-.454 3.665-1.257 5.234a.75.75 0 0 0 .515 1.076 32.91 32.91 0 0 0 3.256.508 3.5 3.5 0 0 0 6.972 0 32.903 32.903 0 0 0 3.256-.508.75.75 0 0 0 .515-1.076A11.448 11.448 0 0 1 16 8a6 6 0 0 0-6-6ZM8.05 14.943a33.54 33.54 0 0 0 3.9 0 2 2 0 0 1-3.9 0Z" clip-rule="evenodd"/>
            """);
    }

    [Theory]
    [InlineData("heroicon-outline")]
    [InlineData("Heroicon-Outline")]
    [InlineData("HEROICON-OUTLINE")]
    public void Should_output_outline_bell_icon(string tagName)
    {
        // Given
        var context = MakeTagHelperContext(tagName);
        var output = MakeTagHelperOutput(tagName);

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "fill", "none");
        AssertAttributeValue(output.Attributes, "stroke", "currentColor");
        AssertAttributeValue(output.Attributes, "stroke-width", "1.5");
        AssertAttributeValue(output.Attributes, "viewbox", "0 0 24 24");
        output.Content.GetContent().ShouldBe(
            """
            <path stroke-linecap="round" stroke-linejoin="round" d="M14.857 17.082a23.848 23.848 0 0 0 5.454-1.31A8.967 8.967 0 0 1 18 9.75V9A6 6 0 0 0 6 9v.75a8.967 8.967 0 0 1-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 0 1-5.714 0m5.714 0a3 3 0 1 1-5.714 0"/>
            """);
    }

    [Theory]
    [InlineData("heroicon-solid")]
    [InlineData("Heroicon-Solid")]
    [InlineData("HEROICON-SOLID")]
    public void Should_output_solid_bell_icon(string tagName)
    {
        // Given
        var context = MakeTagHelperContext(tagName);
        var output = MakeTagHelperOutput(tagName);

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, "fill", "currentColor");
        AssertAttributeValue(output.Attributes, "viewbox", "0 0 24 24");
        output.Attributes.ShouldNotContain(a => a.Name == "stroke");
        output.Attributes.ShouldNotContain(a => a.Name == "stroke-width");
        output.Content.GetContent().ShouldBe(
            """
            <path fill-rule="evenodd" d="M5.25 9a6.75 6.75 0 0 1 13.5 0v.75c0 2.123.8 4.057 2.118 5.52a.75.75 0 0 1-.297 1.206c-1.544.57-3.16.99-4.831 1.243a3.75 3.75 0 1 1-7.48 0 24.585 24.585 0 0 1-4.831-1.244.75.75 0 0 1-.298-1.205A8.217 8.217 0 0 0 5.25 9.75V9Zm4.502 8.9a2.25 2.25 0 1 0 4.496 0 25.057 25.057 0 0 1-4.496 0Z" clip-rule="evenodd"/>
            """);
    }

    [Theory]
    [InlineData("class", "h-6 w-6")]
    [InlineData("style", "height: 32px; width: 32px;")]
    [InlineData("data-icon", "heroicons")]
    public void Should_apply_custom_attributes(string attributeName, string attributeValue)
    {
        // Given
        var context = MakeTagHelperContext(
            "heroicon-solid",
            new TagHelperAttributeList
            {
                { attributeName, attributeValue },
            });
        var output = MakeTagHelperOutput(
            "heroicon-solid",
            new TagHelperAttributeList
            {
                { attributeName, attributeValue },
            });

        var options = Options.Create(new HeroiconOptions());
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        AssertAttributeValue(output.Attributes, attributeName, attributeValue);
    }

    [Fact]
    public void Should_not_include_html_comment_when_IncludeComments_is_false()
    {
        // Given
        var context = MakeTagHelperContext("heroicon-outline");
        var output = MakeTagHelperOutput("heroicon-outline");

        var options = Options.Create(
            new HeroiconOptions
            {
                IncludeComments = false,
            });
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        output.PreElement.GetContent().ShouldBeEmpty();
    }

    [Fact]
    public void Should_include_html_comment_when_IncludeComments_is_true()
    {
        // Given
        var context = MakeTagHelperContext("heroicon-outline");
        var output = MakeTagHelperOutput("heroicon-outline");

        var options = Options.Create(
            new HeroiconOptions
            {
                IncludeComments = true,
            });
        var helper = new IconTagHelper(options)
        {
            Icon = IconSymbol.Bell,
        };

        // When
        helper.Process(context, output);

        // Then
        output.PreElement.GetContent().ShouldBe(
            """
            <!-- Heroicon name: outline bell -->

            """);
    }
}
