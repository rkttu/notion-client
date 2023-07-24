using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.Mention;

namespace NotionSample.Models.Block;

public sealed class NotionDatabaseMentionBlockObject : INotionBlockObject
{
    public NotionDatabaseMentionBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionDatabaseMentionObject Database =>
        new NotionDatabaseMentionObject(JsonElement);

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
