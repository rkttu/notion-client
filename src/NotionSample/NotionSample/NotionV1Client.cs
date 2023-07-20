using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using NotionSample.Models.Page;

namespace NotionSample;

public sealed class NotionV1Client
{
    public NotionV1Client(string notionApiKey, string? notionApiVersion = default)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(
            $"https://api.notion.com/v1/");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", notionApiKey);

        if (!string.IsNullOrWhiteSpace(notionApiVersion))
            client.DefaultRequestHeaders.Add("Notion-Version", notionApiVersion);

        this.HttpClient = client;
    }

    public NotionV1Client(HttpClient httpClient)
    {
        this.HttpClient = httpClient;
    }

    public HttpClient HttpClient { get; private set; }

    public async Task<NotionPageObject?> FetchNotionPage(
        string targetPageId,
        CancellationToken cancellationToken = default)
    {
        var elem = await this.HttpClient.GetFromJsonAsync<JsonElement>(
            $"pages/{targetPageId}",
            cancellationToken)
            .ConfigureAwait(false);

        if (elem.TryGetProperty("object", out JsonElement elem2) &&
            string.Equals(elem2.GetString(), "page", StringComparison.Ordinal))
            return new NotionPageObject(elem);

        return default;
    }
}
