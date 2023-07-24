using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.Mention;

namespace NotionSample.Models.Block;

public sealed class NotionUserMentionBlockObject : INotionBlockObject
{
    public NotionUserMentionBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionUserMentionObject User =>
        new NotionUserMentionObject(JsonElement);

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
