using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionUnsupportedPropertyValueTypeObject : INotionPropertyValueObject
{
    public NotionUnsupportedPropertyValueTypeObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
