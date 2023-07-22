using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

// To Do
public sealed class NotionFilesPropertyValueObject : INotionPropertyValueObject
{
    public NotionFilesPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
