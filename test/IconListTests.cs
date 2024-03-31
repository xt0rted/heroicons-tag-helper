namespace Tailwind.Heroicons;

public class IconListTests
{
    [Fact]
    public void Should_return_the_ArrowDownCircle_icon_in_the_micro_style()
    {
        // Given / When
        var icon = IconList.Micro(IconSymbol.ArrowDownCircle);

        // Then
        icon.Path.ShouldBe(
            """
            <path fill-rule="evenodd" d="M8 15A7 7 0 1 0 8 1a7 7 0 0 0 0 14Zm.75-10.25a.75.75 0 0 0-1.5 0v4.69L6.03 8.22a.75.75 0 0 0-1.06 1.06l2.5 2.5a.75.75 0 0 0 1.06 0l2.5-2.5a.75.75 0 1 0-1.06-1.06L8.75 9.44V4.75Z" clip-rule="evenodd"/>
            """);
        icon.ViewBox.ShouldBe("0 0 16 16");
        icon.StrokeWidth.ShouldBeNull();
    }

    [Fact]
    public void Should_return_the_ArrowDownCircle_icon_in_the_mini_style()
    {
        // Given / When
        var icon = IconList.Mini(IconSymbol.ArrowDownCircle);

        // Then
        icon.Path.ShouldBe(
            """
            <path fill-rule="evenodd" d="M10 18a8 8 0 1 0 0-16 8 8 0 0 0 0 16Zm.75-11.25a.75.75 0 0 0-1.5 0v4.59L7.3 9.24a.75.75 0 0 0-1.1 1.02l3.25 3.5a.75.75 0 0 0 1.1 0l3.25-3.5a.75.75 0 1 0-1.1-1.02l-1.95 2.1V6.75Z" clip-rule="evenodd"/>
            """);
        icon.ViewBox.ShouldBe("0 0 20 20");
        icon.StrokeWidth.ShouldBeNull();
    }

    [Fact]
    public void Should_return_the_ArrowDownCircle_icon_in_the_outline_style()
    {
        // Given / When
        var icon = IconList.Outline(IconSymbol.ArrowDownCircle);

        // Then
        icon.Path.ShouldBe(
            """
            <path stroke-linecap="round" stroke-linejoin="round" d="m9 12.75 3 3m0 0 3-3m-3 3v-7.5M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"/>
            """);
        icon.ViewBox.ShouldBe("0 0 24 24");
        icon.StrokeWidth.ShouldBe("1.5");
    }

    [Fact]
    public void Should_return_the_ArrowDownCircle_icon_in_the_solid_style()
    {
        // Given / When
        var icon = IconList.Solid(IconSymbol.ArrowDownCircle);

        // Then
        icon.Path.ShouldBe(
            """
            <path fill-rule="evenodd" d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25Zm-.53 14.03a.75.75 0 0 0 1.06 0l3-3a.75.75 0 1 0-1.06-1.06l-1.72 1.72V8.25a.75.75 0 0 0-1.5 0v5.69l-1.72-1.72a.75.75 0 0 0-1.06 1.06l3 3Z" clip-rule="evenodd"/>
            """);
        icon.ViewBox.ShouldBe("0 0 24 24");
        icon.StrokeWidth.ShouldBeNull();
    }

    [Fact]
    public void Should_throw_for_unsupported_icon_in_micro_set()
    {
        // Given
#pragma warning disable CS0618 // Type or member is obsolete
        const IconSymbol icon = IconSymbol.PlusSmall;
#pragma warning restore CS0618 // Type or member is obsolete

        // When
        var result = () => IconList.Micro(icon);

        // Then
        var ex = Should.Throw<UnsupportedIconException>(result);
        ex.Style.ShouldBe("micro");
        ex.Name.ShouldBe("plus-small");
    }
}
