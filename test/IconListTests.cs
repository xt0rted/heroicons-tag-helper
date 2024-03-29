﻿namespace Tailwind.Heroicons
{
    public class IconListTests
    {
        [Fact]
        public void Should_return_the_ArrowDownCircle_icon_in_the_outline_style()
        {
            // Given / When
            var icon = IconList.Outline(IconSymbol.ArrowDownCircle);

            // Then
            icon.Path.ShouldBe(
                """
                <path stroke-linecap="round" stroke-linejoin="round" d="M9 12.75l3 3m0 0l3-3m-3 3v-7.5M21 12a9 9 0 11-18 0 9 9 0 0118 0z"/>
                """);
            icon.ViewBox.ShouldBe("0 0 24 24");
            icon.StrokeWidth.ShouldBe("1.5");
        }

        [Fact]
        public void Should_return_the_ArrowDownCircle_icon_in_the_solid_style()
        {
            // Given
            var icon = IconList.Solid(IconSymbol.ArrowDownCircle);

            // When / Then
            icon.Path.ShouldBe(
                """
                <path fill-rule="evenodd" d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25zm-.53 14.03a.75.75 0 001.06 0l3-3a.75.75 0 10-1.06-1.06l-1.72 1.72V8.25a.75.75 0 00-1.5 0v5.69l-1.72-1.72a.75.75 0 00-1.06 1.06l3 3z" clip-rule="evenodd"/>
                """);
            icon.ViewBox.ShouldBe("0 0 24 24");
            icon.StrokeWidth.ShouldBeNull();
        }
    }
}
