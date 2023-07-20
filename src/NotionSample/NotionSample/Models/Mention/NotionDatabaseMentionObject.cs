using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Mention;

public sealed class NotionDatabaseMentionObject : INotionMentionObject
{
    public NotionDatabaseMentionObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Guid? Id =>
        JsonElement.TryGetProperty("database", out JsonElement elem) ?
            elem.TryGetProperty("id", out JsonElement elem2) ?
                elem2.TryGetGuid(out Guid val) ? val : default
            : default
        : default;
}
