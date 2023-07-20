using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.User;

public sealed class NotionPartialUserObject : INotionObject
{
    public NotionPartialUserObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? Id =>
        JsonElement.TryGetProperty("id", out JsonElement elem) ?
            elem.GetString() : default;
}
