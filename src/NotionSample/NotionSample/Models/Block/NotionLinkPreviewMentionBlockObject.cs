using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.Mention;

namespace NotionSample.Models.Block;

public sealed class NotionLinkPreviewMentionBlockObject : INotionBlockObject
{
    public NotionLinkPreviewMentionBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionLinkPreviewMentionObject LinkPreview =>
        new NotionLinkPreviewMentionObject(JsonElement);
}
