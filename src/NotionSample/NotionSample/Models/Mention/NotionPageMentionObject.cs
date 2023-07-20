using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Mention;

public sealed class NotionPageMentionObject : INotionMentionObject
{
    public NotionPageMentionObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Guid? PageId =>
        JsonElement.TryGetProperty("page", out JsonElement elem) ?
            elem.TryGetProperty("id", out JsonElement elem2) ?
                elem2.TryGetGuid(out Guid val) ? val : default
            : default
        : default;
}
