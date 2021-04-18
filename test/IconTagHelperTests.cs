namespace Tailwind.Heroicons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Options;

    using Shouldly;

    using Xunit;

    public class IconTagHelperTests
    {
        [Fact]
        public void Should_set_svg_attributes()
        {
            // Given
            var context = MakeTagHelperContext(tagName: "heroicon-outline");
            var output = MakeTagHelperOutput(tagName: "heroicon-outline");

            var options = Options.Create(new HeroiconOptions());
            var helper = new IconTagHelper(options);

            // When
            helper.Process(context, output);

            // Then
            output.TagMode.ShouldBe(TagMode.StartTagAndEndTag);
            output.TagName.ShouldBe("svg");
            AssertAttributeValue(output.Attributes, "aria-hidden", "true");
            AssertAttributeValue(output.Attributes, "viewbox", "0 0 24 24");
        }

        [Fact]
        public void Should_overwrite_existing_attributes_for_solid_style()
        {
            // Given
            var context = MakeTagHelperContext(
                tagName: "heroicon-solid",
                new TagHelperAttributeList
                {
                    { "fill", "blue" },
                    { "viewbox", "0 0 1 1" },
                });
            var output = MakeTagHelperOutput(
                tagName: "heroicon-solid",
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
            AssertAttributeValue(output.Attributes, "viewbox", "0 0 20 20");
            output.Attributes.ShouldNotContain(a => a.Name == "stroke");
        }

        [Fact]
        public void Should_overwrite_existing_attributes_for_outline_style()
        {
            // Given
            var context = MakeTagHelperContext(
                tagName: "heroicon-outline",
                new TagHelperAttributeList
                {
                    { "fill", "blue" },
                    { "stroke", "blue" },
                    { "viewbox", "0 0 1 1" },
                });
            var output = MakeTagHelperOutput(
                tagName: "heroicon-outline",
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
            AssertAttributeValue(output.Attributes, "viewbox", "0 0 24 24");
            output.Content.GetContent().ShouldBe("<path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9\"/>");
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
            AssertAttributeValue(output.Attributes, "viewbox", "0 0 20 20");
            output.Attributes.ShouldNotContain(a => a.Name == "stroke");
            output.Content.GetContent().ShouldBe("<path d=\"M10 2a6 6 0 00-6 6v3.586l-.707.707A1 1 0 004 14h12a1 1 0 00.707-1.707L16 11.586V8a6 6 0 00-6-6zM10 18a3 3 0 01-3-3h6a3 3 0 01-3 3z\"/>");
        }

        [Theory]
        [InlineData("class", "h-6 w-6")]
        [InlineData("style", "height: 32px; width: 32px;")]
        [InlineData("data-icon", "heroicons")]
        public void Should_apply_custom_attributes(string attributeName, string attributeValue)
        {
            // Given
            var context = MakeTagHelperContext(
                tagName: "heroicon-solid",
                new TagHelperAttributeList
                {
                    { attributeName, attributeValue },
                });
            var output = MakeTagHelperOutput(
                tagName: "heroicon-solid",
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
            var context = MakeTagHelperContext(tagName: "heroicon-outline");
            var output = MakeTagHelperOutput(tagName: "heroicon-outline");

            var options = Options.Create(new HeroiconOptions
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
            var context = MakeTagHelperContext(tagName: "heroicon-outline");
            var output = MakeTagHelperOutput(tagName: "heroicon-outline");

            var options = Options.Create(new HeroiconOptions
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
            output.PreElement.GetContent().ShouldBe("<!-- Heroicon name: outline bell -->");
        }

        private static TagHelperContext MakeTagHelperContext(string tagName, TagHelperAttributeList attributes = null)
        {
            attributes ??= new TagHelperAttributeList();

            return new TagHelperContext(
                tagName,
                allAttributes: attributes,
                items: new Dictionary<object, object>(),
                uniqueId: Guid.NewGuid().ToString("N"));
        }

        private static TagHelperOutput MakeTagHelperOutput(string tagName, TagHelperAttributeList attributes = null)
        {
            attributes ??= new TagHelperAttributeList();

            return new TagHelperOutput(
                tagName,
                attributes: attributes,
                getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
        }

        private static void AssertAttributeValue(TagHelperAttributeList attributes, string name, string value)
        {
            attributes
                .Count(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ShouldBe(1);

            attributes[name].Value.ShouldBe(value);
        }
    }
}
