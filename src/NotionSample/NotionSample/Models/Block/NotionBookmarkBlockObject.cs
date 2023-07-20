using System;
using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionBookmarkBlockObject : INotionBlockObject
{
    public NotionBookmarkBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> Caption =>
        JsonElement.TryGetProperty("bookmark", out JsonElement elem) ?
            elem.TryGetProperty("caption", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public Uri? Url =>
        JsonElement.TryGetProperty("bookmark", out JsonElement elem) ?
            elem.TryGetProperty("url", out JsonElement elem2) ?
                Uri.TryCreate(elem2.GetString(), UriKind.Absolute, out Uri? val) ? val : default
            : default
        : default;
}
