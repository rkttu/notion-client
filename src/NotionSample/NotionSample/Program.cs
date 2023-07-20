using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using NotionSample.Models.Page;

namespace NotionSample;

public sealed class Program
{
    private static readonly string NotionApiVersion = "2022-06-28";

    [STAThread]
    private static async Task Main(string[] _)
    {
        var keyFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "notion_api_key.txt");
        var notionApiKey = await File.ReadAllTextAsync(
            keyFilePath, new UTF8Encoding(false));
        notionApiKey = notionApiKey.TrimEnd('\r', '\n');

        var client = new HttpClient();
        client.BaseAddress = new Uri(
            $"https://api.notion.com/v1/");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", notionApiKey);
        client.DefaultRequestHeaders.Add("Notion-Version", NotionApiVersion);

        var targetPageId = "56beb7b10d324c27a5c13493c92441e3";
        var jsonElem = await client.GetFromJsonAsync<JsonElement>(
            $"pages/{targetPageId}");
        var page = new NotionPageObject(jsonElem);

        await Console.Out.WriteLineAsync($"Page URL: {page.Url}");
        await Console.Out.WriteLineAsync($"Page Icon Type: {page.Icon?.Type}");
        return;
    }
}
