namespace Tailwind.Heroicons;

/// <summary>
/// Global settings used when emitting Heroicons.
/// </summary>
public class HeroiconOptions
{
    /// <summary>
    /// Add an html comment before the svg tag with the style and name of the icon to help make development/debugging easier.
    /// </summary>
    /// <remarks>This is off by default.</remarks>
    public bool IncludeComments { get; set; }

    /// <summary>
    /// Adds various accessibility attributes based on the default state of the tag.
    /// </summary>
    /// <remarks>This is off by default.</remarks>
    public bool SetAccessibilityAttributes { get; set; }

    /// <summary>
    /// Adds the <c>focusable</c> attribute set to <c>false</c> to prevent the icon from receiving focus in Internet Explorer and Edge Legacy.
    /// </summary>
    /// <remarks>This is off by default.</remarks>
    public bool SetFocusableAttribute { get; set; }
}
