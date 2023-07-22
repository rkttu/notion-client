using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionCheckBoxPropertyValueObject : INotionPropertyValueObject
{
    public NotionCheckBoxPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public bool? Checked =>
        JsonElement.TryGetProperty("checkbox", out JsonElement elem) ?
            elem.GetBoolean()
        : default;
}
