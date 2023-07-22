using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.User;

public sealed class NotionUnsupportedUserObject : INotionUserObject
{
    public NotionUnsupportedUserObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
