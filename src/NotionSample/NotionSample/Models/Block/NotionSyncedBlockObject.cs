using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionSyncedBlockObject : INotionBlockObject
{
    public NotionSyncedBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionSyncedFromObject? SyncedFrom =>
        JsonElement.TryGetProperty("synced_block", out JsonElement elem) ?
            elem.TryGetProperty("synced_from", out JsonElement elem2) ?
                new NotionSyncedFromObject(elem2) : default
            : default;

    public IEnumerable<INotionBlockObject?> Children =>
        JsonElement.TryGetProperty("synced_block", out JsonElement elem) ?
            elem.TryGetProperty("children", out JsonElement elem2) ?
                elem2.ToNotionBlockObjects() : Enumerable.Empty<INotionBlockObject?>()
            : Enumerable.Empty<INotionBlockObject?>();
}
