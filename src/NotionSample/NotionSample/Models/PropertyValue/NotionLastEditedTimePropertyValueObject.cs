using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionLastEditedTimePropertyValueObject : INotionPropertyValueObject
{
    public NotionLastEditedTimePropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public DateTimeOffset? LastEditedTime =>
        JsonElement.TryGetProperty("last_edited_time", out JsonElement elem) ?
            elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default
        : default;
}
