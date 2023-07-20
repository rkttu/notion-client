using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.PropertyValue;

namespace NotionSample.Models.Mention;

public sealed class NotionDateMentionObject : INotionMentionObject
{
    public NotionDateMentionObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionDatePropertyValueObject? Date =>
        JsonElement.TryGetProperty("date", out JsonElement elem) ?
            new NotionDatePropertyValueObject(elem)
        : default;
}
