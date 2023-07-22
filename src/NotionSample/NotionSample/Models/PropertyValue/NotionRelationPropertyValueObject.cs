using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

// To DO
public sealed class NotionRelationPropertyValueObject : INotionPropertyValueObject
{
    public NotionRelationPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
