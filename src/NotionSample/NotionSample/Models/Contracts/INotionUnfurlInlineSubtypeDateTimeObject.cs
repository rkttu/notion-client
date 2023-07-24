using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUnfurlInlineSubtypeDateTimeObject : IJsonWrappedObject
{
    public DateTimeOffset? DateTimeValue =>
        JsonElement.TryGetProperty("datetime", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default
            : default
        : default;

    public string? Section =>
        JsonElement.TryGetProperty("datetime", out JsonElement elem) ?
            elem.TryGetProperty("section", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
