﻿using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionHeadingLv1BlockObject : INotionBlockObject
{
    public NotionHeadingLv1BlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> Text =>
        JsonElement.TryGetProperty("heading_1", out JsonElement elem) ?
            elem.TryGetProperty("text", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public string? Color =>
        JsonElement.TryGetProperty("heading_1", out JsonElement elem) ?
            elem.TryGetProperty("color", out JsonElement elem2) ?
                elem2.GetString() : default
            : default;

    public bool? IsToggleable =>
        JsonElement.TryGetProperty("heading_1", out JsonElement elem) ?
            elem.TryGetProperty("is_toggleable", out JsonElement elem2) ?
                elem2.GetBoolean() : default
            : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
