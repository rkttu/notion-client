using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionEmailPropertyValueObject : INotionPropertyValueObject
{
    public NotionEmailPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Uri? Email =>
        JsonElement.TryGetProperty("url", out JsonElement elem) ?
            Uri.TryCreate(string.Concat("mailto:", elem.GetString()), UriKind.Absolute, out Uri? val) ? val : default
        : default;
}
