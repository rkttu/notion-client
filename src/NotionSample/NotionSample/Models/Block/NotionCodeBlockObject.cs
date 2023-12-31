﻿using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionCodeBlockObject : INotionBlockObject
{
    public NotionCodeBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> Caption =>
        JsonElement.TryGetProperty("code", out JsonElement elem) ?
            elem.TryGetProperty("caption", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public IEnumerable<INotionRichTextObject?> Text =>
        JsonElement.TryGetProperty("code", out JsonElement elem) ?
            elem.TryGetProperty("text", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public string? Title =>
        JsonElement.TryGetProperty("code", out JsonElement elem) ?
            elem.TryGetProperty("language", out JsonElement elem2) ?
                elem2.GetString() : default
            : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
