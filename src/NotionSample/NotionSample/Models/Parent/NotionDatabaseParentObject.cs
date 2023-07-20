using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Parent;

public sealed class NotionDatabaseParentObject : INotionTypedObject
{
    public NotionDatabaseParentObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Guid? DatabaseId =>
        JsonElement.TryGetProperty("database_id", out JsonElement elem) ?
            elem.TryGetGuid(out Guid val) ? val : default
        : default;
}
