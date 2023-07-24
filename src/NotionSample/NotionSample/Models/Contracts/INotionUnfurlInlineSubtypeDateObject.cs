using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUnfurlInlineSubtypeDateObject : IJsonWrappedObject
{
    public DateTimeOffset? DateValue =>
        JsonElement.TryGetProperty("date", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default
            : default
        : default;

    public string? Section =>
        JsonElement.TryGetProperty("date", out JsonElement elem) ?
            elem.TryGetProperty("section", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
