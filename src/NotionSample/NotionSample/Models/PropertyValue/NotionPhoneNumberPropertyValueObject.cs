using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionPhoneNumberPropertyValueObject : INotionPropertyValueObject
{
    public NotionPhoneNumberPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? PhoneNumber =>
        JsonElement.TryGetProperty("phone_number", out JsonElement elem) ?
            elem.GetString()
        : default;
}
