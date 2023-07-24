using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using NotionSample.Models;
using NotionSample.Models.Block;
using NotionSample.Models.Contracts;
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

    public async IAsyncEnumerable<INotionBlockObject?> FetchChildNotionBlocks(
        string? blockId,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (blockId == null)
            yield break;

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
                yield return eachBlockObject;

            var hasMore = elem.TryGetProperty("has_more", out JsonElement elem2) ?
                elem2.GetBoolean() : false;

            nextCursor = elem.TryGetProperty("next_cursor", out JsonElement elem3) ?
                elem3.GetString() : null;

            if (!hasMore || string.IsNullOrWhiteSpace(nextCursor))
                break;
        }
    }

    public async IAsyncEnumerable<INotionBlockObject?> FetchAllChildNotionBlocks(
        string? blockId,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (blockId == null)
            yield break;

        var queue = new Queue<INotionBlockObject?>();

        await foreach (var eachItem in FetchChildNotionBlocks(blockId, cancellationToken).ConfigureAwait(false))
        {
            queue.Enqueue(eachItem);
            continue;
        }

        while (queue.TryDequeue(out INotionBlockObject? childBlock))
        {
            if (childBlock == null)
                continue;

            yield return childBlock;

            await foreach (var eachChildObject in FetchChildNotionBlocks(childBlock.Id?.ToString("D"), cancellationToken).ConfigureAwait(false))
            {
                childBlock.Children.Add(eachChildObject);
                queue.Enqueue(eachChildObject);
            }
        }
    }
}
