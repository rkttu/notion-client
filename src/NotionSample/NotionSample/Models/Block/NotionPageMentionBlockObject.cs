using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.Mention;

namespace NotionSample.Models.Block;

public sealed class NotionPageMentionBlockObject : INotionBlockObject
{
    public NotionPageMentionBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionPageMentionObject Page =>
        new NotionPageMentionObject(JsonElement);
}
