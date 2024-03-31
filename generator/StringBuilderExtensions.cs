namespace IconSourceGenerator;

internal static class StringBuilderExtensions
{
    public static StringBuilder AppendLine(this StringBuilder builder, params string[] value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            builder.Append(value[i]);
        }

        return builder.AppendLine();
    }
}
