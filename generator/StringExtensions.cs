namespace IconSourceGenerator
{
    internal static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            var arr = input.ToCharArray();

            arr[0] = char.ToUpperInvariant(arr[0]);

            return new string(arr);
        }

        public static string ToPascalCase(this string name)
        {
            var splitName = name.Split('-');

            for (var i = 0; i < splitName.Length; i++)
            {
                splitName[i] = FirstCharToUpper(splitName[i]);
            }

            return string.Join(string.Empty, splitName);
        }
    }
}
