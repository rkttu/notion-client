using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Parent;

public sealed class NotionWorkspaceParentObject : INotionTypedObject
{
    public NotionWorkspaceParentObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public bool? Workspace =>
        JsonElement.TryGetProperty("workspace", out JsonElement elem) ?
            elem.GetBoolean()
        : default;
}
