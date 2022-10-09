namespace Tailwind.Heroicons
{
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
                <path stroke-linecap="round" stroke-linejoin="round" d="M14.857 17.082a23.848 23.848 0 005.454-1.31A8.967 8.967 0 0118 9.75v-.7V9A6 6 0 006 9v.75a8.967 8.967 0 01-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 01-5.714 0m5.714 0a3 3 0 11-5.714 0"/>
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
                <path fill-rule="evenodd" d="M5.25 9a6.75 6.75 0 0113.5 0v.75c0 2.123.8 4.057 2.118 5.52a.75.75 0 01-.297 1.206c-1.544.57-3.16.99-4.831 1.243a3.75 3.75 0 11-7.48 0 24.585 24.585 0 01-4.831-1.244.75.75 0 01-.298-1.205A8.217 8.217 0 005.25 9.75V9zm4.502 8.9a2.25 2.25 0 104.496 0 25.057 25.057 0 01-4.496 0z" clip-rule="evenodd"/>
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
}
