using NotionSample.Models;
using NotionSample.Models.Contracts;
using NotionSample.Models.Page;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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

    public async Task<NotionPageObject?> RetrieveNotionPage(
        string pageId,
        CancellationToken cancellationToken = default)
    {
        var elem = await this.HttpClient.GetFromJsonAsync<JsonElement>(
            $"pages/{pageId}",
            cancellationToken)
            .ConfigureAwait(false);

        if (elem.TryGetProperty("object", out JsonElement elem2) &&
            string.Equals(elem2.GetString(), "page", StringComparison.Ordinal))
            return new NotionPageObject(elem);

        return default;
    }

    public async Task<NotionDatabaseObject?> RetrieveNotionDatabase(
        string databaseId,
        CancellationToken cancellationToken = default)
    {
        var elem = await this.HttpClient.GetFromJsonAsync<JsonElement>(
            $"databases/{databaseId}",
            cancellationToken)
            .ConfigureAwait(false);

        if (elem.TryGetProperty("object", out JsonElement elem2) &&
            string.Equals(elem2.GetString(), "database", StringComparison.Ordinal))
            return new NotionDatabaseObject(elem);

        return default;
    }

    public async Task<INotionBlockObject?> RetrieveNotionBlock(
        string blockId,
        CancellationToken cancellationToken = default)
    {
        var elem = await this.HttpClient.GetFromJsonAsync<JsonElement>(
            $"blocks/{blockId}",
            cancellationToken)
            .ConfigureAwait(false);

        if (elem.TryGetProperty("object", out JsonElement elem2))
            return elem.CreateNotionBlockObject();

        return default;
    }

    public async Task<INotionBlockObject> FetchChildNotionBlocks(
        INotionBlockObject blockObject,
        CancellationToken cancellationToken = default)
    {
        string? blockId = blockObject.Id?.ToString("D");

        if (blockId == null)
            return blockObject;

        string? nextCursor = null;
        JsonElement elem;
        IEnumerable<INotionBlockObject?> results;
        int pageSize = 100;

        while (true)
        {
            elem = await this.HttpClient.GetFromJsonAsync<JsonElement>(
                $"blocks/{blockId}/children?" +
                (nextCursor != null ? $"start_cursor={Uri.EscapeDataString(nextCursor)}&" : string.Empty) +
                $"page_size={Uri.EscapeDataString(pageSize.ToString())}",
                cancellationToken)
                .ConfigureAwait(false);

            results = elem.TryGetProperty("results", out JsonElement elem1) ?
                elem1.ToNotionBlockObjects() :
                Enumerable.Empty<INotionBlockObject?>();

            foreach (var eachBlockObject in results)
            {
                await Console.Out.WriteLineAsync($"[{eachBlockObject?.GetType().Name}]");
                blockObject.Children.Add(eachBlockObject);
            }

            var hasMore = elem.TryGetProperty("has_more", out JsonElement elem2) ?
                elem2.GetBoolean() : false;

            nextCursor = elem.TryGetProperty("next_cursor", out JsonElement elem3) ?
                elem3.GetString() : null;

            if (!hasMore || string.IsNullOrWhiteSpace(nextCursor))
                break;
        }

        return blockObject;
    }

    public async Task<INotionBlockObject?> FetchAllChildNotionBlocks(
        INotionBlockObject blockObject,
        Func<INotionBlockObject?, CancellationToken, Task>? callback = default,
        CancellationToken cancellationToken = default)
    {
        await FetchChildNotionBlocks(blockObject, cancellationToken).ConfigureAwait(false);

        foreach (var eachChildObject in blockObject.Children)
        {
            if (eachChildObject == null)
                continue;

            await FetchAllChildNotionBlocks(eachChildObject, callback, cancellationToken).ConfigureAwait(false);

            if (callback != null)
                await callback.Invoke(eachChildObject, cancellationToken).ConfigureAwait(false);
        }

        return blockObject;
    }
}
