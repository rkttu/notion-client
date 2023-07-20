using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Parent;

public sealed class NotionPageParentObject : INotionTypedObject
{
    public NotionPageParentObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Guid? PageId =>
        JsonElement.TryGetProperty("page_id", out JsonElement elem) ?
            elem.TryGetGuid(out Guid val) ? val : default
        : default;
}
