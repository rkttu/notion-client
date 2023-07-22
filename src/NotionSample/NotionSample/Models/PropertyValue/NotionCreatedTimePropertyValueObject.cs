using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionCreatedTimePropertyValueObject : INotionPropertyValueObject
{
    public NotionCreatedTimePropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public DateTimeOffset? CreatedTime =>
        JsonElement.TryGetProperty("created_time", out JsonElement elem) ?
            elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default
        : default;
}
