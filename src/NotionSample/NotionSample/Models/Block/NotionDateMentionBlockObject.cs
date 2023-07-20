using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.Mention;

namespace NotionSample.Models.Block;

public sealed class NotionDateMentionBlockObject : INotionBlockObject
{
    public NotionDateMentionBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionDateMentionObject Date =>
        new NotionDateMentionObject(JsonElement);
}
