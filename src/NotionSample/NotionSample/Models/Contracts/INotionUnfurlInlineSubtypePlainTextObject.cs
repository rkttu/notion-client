using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUnfurlInlineSubtypePlainTextObject : IJsonWrappedObject
{
    public string? TextValue =>
        JsonElement.TryGetProperty("plain_text", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;

    public string? Section =>
        JsonElement.TryGetProperty("plain_text", out JsonElement elem) ?
            elem.TryGetProperty("section", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
