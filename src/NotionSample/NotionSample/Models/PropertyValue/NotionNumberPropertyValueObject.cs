using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionNumberPropertyValueObject : INotionPropertyValueObject
{
    public NotionNumberPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public decimal? Number =>
        JsonElement.TryGetProperty("number", out JsonElement elem) ?
            elem.TryGetDecimal(out decimal val) ? val : default
        : default;
}
