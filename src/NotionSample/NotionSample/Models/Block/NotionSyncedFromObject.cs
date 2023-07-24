using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionSyncedFromObject : INotionTypedObject
{
    public NotionSyncedFromObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Guid? BlockId =>
        JsonElement.TryGetProperty("block_id", out JsonElement elem) ?
            elem.TryGetGuid(out Guid val) ? val : default
        : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
