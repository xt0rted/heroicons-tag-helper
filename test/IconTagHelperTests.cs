namespace Tailwind.Heroicons
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Razor.TagHelpers;

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

            var helper = new IconTagHelper();

            // When
            helper.Process(context, output);

            // Then
            output.TagMode.ShouldBe(TagMode.StartTagAndEndTag);
            output.TagName.ShouldBe("svg");
            output.Attributes.ShouldContain(new TagHelperAttribute("aria-hidden", "true"));
            output.Attributes.ShouldContain(new TagHelperAttribute("viewbox", "0 0 24 24"));
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

            var helper = new IconTagHelper
            {
                Icon = IconSymbol.Bell
            };

            // When
            helper.Process(context, output);

            // Then
            output.Attributes.ShouldContain(new TagHelperAttribute("fill", "none"));
            output.Attributes.ShouldContain(new TagHelperAttribute("stroke", "currentColor"));
            output.Attributes.ShouldContain(new TagHelperAttribute("viewbox", "0 0 24 24"));
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

            var helper = new IconTagHelper
            {
                Icon = IconSymbol.Bell
            };

            // When
            helper.Process(context, output);

            // Then
            output.Attributes.ShouldContain(new TagHelperAttribute("fill", "currentColor"));
            output.Attributes.ShouldContain(new TagHelperAttribute("viewbox", "0 0 20 20"));
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

            var helper = new IconTagHelper
            {
                Icon = IconSymbol.Bell
            };

            // When
            helper.Process(context, output);

            // Then
            output.Attributes.ShouldContain(new TagHelperAttribute(attributeName, attributeValue));
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
    }
}
