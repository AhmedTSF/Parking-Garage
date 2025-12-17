using System.Text;


namespace Domain.Extensions;

public static class StringExtension
{
    public static string ToSpaced(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var result = new StringBuilder();
        result.Append(text[0]);

        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && !char.IsWhiteSpace(text[i - 1]))
                result.Append(' ');

            result.Append(text[i]);
        }

        return result.ToString();
    }
}
