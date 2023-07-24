using NotionSample.Models.Contracts;
using System.Text.Json;

namespace NotionSample.Models.LinkPreview;

public sealed class NotionUnfurlAttributeObject : INotionTypedObject
{
    public NotionUnfurlAttributeObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? Id =>
        JsonElement.TryGetProperty("id", out JsonElement elem) ?
            elem.GetString()
        : default;

    public string? Name =>
        JsonElement.TryGetProperty("name", out JsonElement elem) ?
            elem.GetString()
        : default;

    public bool? IsDevAttribute =>
        Id != null ? string.Equals(Id, "dev", StringComparison.Ordinal) : default;

    public NotionUnfurlInlineSubtypeObject? Inline =>
        JsonElement.TryGetProperty("inline", out JsonElement elem) ?
            new NotionUnfurlInlineSubtypeObject(elem)
        : default;

    public NotionUnfurlEmbedSubtypeObject? Embed =>
        JsonElement.TryGetProperty("embed", out JsonElement elem) ?
            new NotionUnfurlEmbedSubtypeObject(elem)
        : default;
}
