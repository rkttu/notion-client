using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionUrlPropertyValueObject : INotionPropertyValueObject
{
    public NotionUrlPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Uri? Url =>
        JsonElement.TryGetProperty("url", out JsonElement elem) ?
            Uri.TryCreate(elem.GetString(), UriKind.Absolute, out Uri? val) ? val : default
        : default;
}
