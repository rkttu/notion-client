﻿using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionToggleBlockObject : INotionBlockObject
{
    public NotionToggleBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> RichText =>
        JsonElement.TryGetProperty("toggle", out JsonElement elem) ?
            elem.TryGetProperty("rich_text", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public string? Color =>
        JsonElement.TryGetProperty("toggle", out JsonElement elem) ?
            elem.TryGetProperty("color", out JsonElement elem2) ?
                elem2.GetString() : default
            : default;

    /*
    public IEnumerable<INotionBlockObject?> Children =>
        JsonElement.TryGetProperty("toggle", out JsonElement elem) ?
            elem.TryGetProperty("children", out JsonElement elem2) ?
                elem2.ToNotionBlockObjects() : Enumerable.Empty<INotionBlockObject?>()
            : Enumerable.Empty<INotionBlockObject?>();
    */

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
