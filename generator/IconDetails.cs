namespace IconSourceGenerator;

internal class IconDetails
{
    public string Path { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public string Style { get; set; } = null!;

    public bool UsesStroke { get; set; }

    public AdditionalText File { get; set; } = null!;
}
