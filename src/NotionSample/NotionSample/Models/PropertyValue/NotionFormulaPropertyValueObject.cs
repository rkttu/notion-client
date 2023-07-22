using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

// To Do
public sealed class NotionFormulaPropertyValueObject : INotionPropertyValueObject
{
    public NotionFormulaPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
