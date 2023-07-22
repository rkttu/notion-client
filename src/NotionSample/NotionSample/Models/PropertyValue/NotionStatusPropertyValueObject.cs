using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionStatusPropertyValueObject : INotionPropertyValueObject
{
    public NotionStatusPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Guid? Id =>
        JsonElement.TryGetProperty("id", out JsonElement elem) ?
            elem.TryGetGuid(out Guid val) ? val : default
        : default;

    public string? Name =>
        JsonElement.TryGetProperty("name", out JsonElement elem) ?
            elem.GetString()
        : default;

    public string? Color =>
        JsonElement.TryGetProperty("color", out JsonElement elem) ?
            elem.GetString()
        : default;
}
