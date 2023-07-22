using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Parent;

public sealed class NotionUnsupportedParentObject : INotionTypedObject
{
    public NotionUnsupportedParentObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
