using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Mention;

public sealed class NotionUnsupportedMentionObject : INotionMentionObject
{
    public NotionUnsupportedMentionObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
