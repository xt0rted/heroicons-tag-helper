namespace Tailwind.Heroicons
{
    using Shouldly;

    using Xunit;

    public class IconListTests
    {
        [Fact]
        public void Should_return_the_ArrowCircleDown_icon_in_the_outline_style()
        {
            // Given / When
            var icon = IconList.Outline(IconSymbol.ArrowCircleDown);

            // Then
            icon.Path.ShouldBe("<path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M15 13l-3 3m0 0l-3-3m3 3V8m0 13a9 9 0 110-18 9 9 0 010 18z\"/>");
            icon.ViewBox.ShouldBe("0 0 24 24");
        }

        [Fact]
        public void Should_return_the_ArrowCircleDown_icon_in_the_solid_style()
        {
            // Given
            var icon = IconList.Solid(IconSymbol.ArrowCircleDown);

            // When / Then
            icon.Path.ShouldBe("<path fill-rule=\"evenodd\" d=\"M10 18a8 8 0 100-16 8 8 0 000 16zm1-11a1 1 0 10-2 0v3.586L7.707 9.293a1 1 0 00-1.414 1.414l3 3a1 1 0 001.414 0l3-3a1 1 0 00-1.414-1.414L11 10.586V7z\" clip-rule=\"evenodd\"/>");
            icon.ViewBox.ShouldBe("0 0 20 20");
        }
    }
}
