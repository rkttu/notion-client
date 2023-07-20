using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Parent;

public sealed class NotionBlockParentObject : INotionTypedObject
{
    public NotionBlockParentObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Guid? BlockId =>
        JsonElement.TryGetProperty("block_id", out JsonElement elem) ?
            elem.TryGetGuid(out Guid val) ? val : default
        : default;
}
