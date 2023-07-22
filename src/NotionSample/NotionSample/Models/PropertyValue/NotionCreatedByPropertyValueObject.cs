using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

// To Do
public sealed class NotionCreatedByPropertyValueObject : INotionPropertyValueObject
{
    public NotionCreatedByPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    // created_by
}
