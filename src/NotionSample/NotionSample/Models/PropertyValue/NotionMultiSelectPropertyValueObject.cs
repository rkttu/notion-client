using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionMultiSelectPropertyValueObject : INotionPropertyValueObject
{
    public NotionMultiSelectPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<NotionSelectPropertyValueObject>? SelectedValues =>
        JsonElement.TryGetProperty("multi_select", out JsonElement elem) ?
            elem.EnumerateArray().Select(x => new NotionSelectPropertyValueObject(x))
        : default;
}
