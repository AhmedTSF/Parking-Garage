using System.Security.Cryptography.X509Certificates;
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

    public static bool IsValidPlateNumber(string plateNumber) 
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
            return false;

        // Must be exactly 7 characters: 3 letters + '-' + 3 digits
        if (plateNumber.Length != 7)
            return false;

        // Check first 3 characters are uppercase letters
        for (int i = 0; i < 3; i++)
        {
            if (!char.IsLetter(plateNumber[i]) || !char.IsUpper(plateNumber[i]))
                return false;
        }

        // Check 4th character is '-'
        if (plateNumber[3] != '-')
            return false;

        // Check last 3 characters are digits
        for (int i = 4; i < 7; i++)
        {
            if (!char.IsDigit(plateNumber[i]))
                return false;
        }

        return true;
    }

    public static bool IsValidNationalId(string nationalId)
    {

        if (!string.IsNullOrEmpty(nationalId) || nationalId.Length <= 0 || nationalId.Length > 4)
            return false;

        const int maxLength = 4; 
        for(int i = 0; i < maxLength; i++)
        {
            if(!char.IsDigit(nationalId[i]))
                return false;
        }

        return true;
    }

}
