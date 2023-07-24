using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUnfurlInlineSubtypeTitleObject : IJsonWrappedObject
{
    public string? TitleValue =>
        JsonElement.TryGetProperty("title", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;

    public string? Section =>
        JsonElement.TryGetProperty("title", out JsonElement elem) ?
            elem.TryGetProperty("section", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
