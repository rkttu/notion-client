using System.Text;
using System.Text.RegularExpressions;

namespace NotionSample;

internal static class Helpers
{
    private static readonly string NotionApiKeyFileName = "notion_api_key.txt";

    public static async Task<string?> LoadApiKeyFromUserDirectory(
        string? apiKeyFileName = default,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(apiKeyFileName))
            apiKeyFileName = NotionApiKeyFileName;

        var keyFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            apiKeyFileName);

        var notionApiFileContent = await File.ReadAllTextAsync(
            keyFilePath, new UTF8Encoding(false),
            cancellationToken);

        var secretKeyMatch = Regex.Match(
            notionApiFileContent, "secret_.+", RegexOptions.Compiled);

        if (secretKeyMatch.Success)
            return secretKeyMatch.Value;

        return default;
    }
}
