using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.RichText;

public sealed class NotionMentionRichTextObject : INotionRichTextObject
{
    public NotionMentionRichTextObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public INotionMentionObject? GetNotionMentionObject =>
        JsonElement.TryGetProperty("mention", out JsonElement elem) ?
            elem.CreateNotionMentionObject()
        : default;
}
