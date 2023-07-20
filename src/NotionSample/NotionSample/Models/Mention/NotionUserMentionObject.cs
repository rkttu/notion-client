using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.User;

namespace NotionSample.Models.Mention;

public sealed class NotionUserMentionObject : INotionMentionObject
{
    public NotionUserMentionObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionPartialUserObject? User =>
        JsonElement.TryGetProperty("user", out JsonElement elem) ?
            new NotionPartialUserObject(elem)
        : default;
}
