using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Attachments;

public sealed class NotionEmojiObject : INotionTypedObject
{
    public NotionEmojiObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? Emoji =>
        JsonElement.TryGetProperty("emoji", out JsonElement elem) ?
            elem.GetString()
        : default;
}
