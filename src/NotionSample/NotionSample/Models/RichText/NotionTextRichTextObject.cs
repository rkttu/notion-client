using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.RichText;

public sealed class NotionTextRichTextObject : INotionRichTextObject
{
    public NotionTextRichTextObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? TextContent =>
        JsonElement.TryGetProperty("text", out JsonElement elem) ?
            elem.TryGetProperty("content", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
