using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

// To Do
public sealed class NotionLastEditedByPropertyValueObject : INotionPropertyValueObject
{
    public NotionLastEditedByPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    // last_edited_by
}
