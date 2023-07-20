using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionDatePropertyValueObject : INotionPropertyValueObject
{
    public NotionDatePropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public DateTimeOffset? Start =>
        JsonElement.TryGetProperty("start", out JsonElement elem) ?
            elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default
        : default;

    public DateTimeOffset? End =>
        JsonElement.TryGetProperty("end", out JsonElement elem) ?
            elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default
        : default;

    public string? TimeZone =>
        JsonElement.TryGetProperty("time_zone", out JsonElement elem) ?
            elem.GetString()
        : default;
}
