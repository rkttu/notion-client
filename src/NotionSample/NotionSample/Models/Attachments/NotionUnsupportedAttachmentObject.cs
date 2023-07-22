using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Attachments;

public sealed class NotionUnsupportedAttachmentObject : INotionTypedObject
{
    public NotionUnsupportedAttachmentObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
