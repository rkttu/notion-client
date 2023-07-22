using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.RichText;

public sealed class NotionUnsupportedRichTextObject : INotionRichTextObject
{
    public NotionUnsupportedRichTextObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
