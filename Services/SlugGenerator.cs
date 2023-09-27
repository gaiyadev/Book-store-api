using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BookstoreAPI.Services;
public class SlugGenerator
{
    private static readonly Random Random = new Random();

    public  string GenerateSlug(string input)
    {
        // Remove any special characters, diacritics, or spaces
        string normalized = RemoveDiacritics(input).ToLower();

        // Replace spaces with hyphens and remove other special characters
        normalized = Regex.Replace(normalized, @"[^a-z0-9\s-]", "");

        // Replace spaces with hyphens
        normalized = normalized.Replace(' ', '-');

        // Replace multiple hyphens with a single hyphen
        normalized = Regex.Replace(normalized, @"-+", "-");

        // Trim hyphens from the start and end
        normalized = normalized.Trim('-');

        // Generate a random number (e.g., between 1000 and 9999)
        int randomNumber = Random.Next(1000, 10000);
        
        // Append the current date and time in a specific format
        string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        // Append the random number to the slug
        string slugWithRandom = $"{normalized}-{randomNumber}-${currentDateTime}";
        
        return slugWithRandom;
    }

    private static string RemoveDiacritics(string input)
    {
        string normalized = input.Normalize(NormalizationForm.FormD);
        StringBuilder builder = new StringBuilder();

        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }
}