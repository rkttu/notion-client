using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.User;

public sealed class NotionBotUserObject : INotionUserObject
{
    public NotionBotUserObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? OwnerType =>
        JsonElement.TryGetProperty("owner", out JsonElement elem) ?
            elem.TryGetProperty("type", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;

    public bool? OwnerWorkspace =>
        JsonElement.TryGetProperty("owner", out JsonElement elem) ?
            elem.TryGetProperty("workspace", out JsonElement elem2) ?
                elem2.GetBoolean()
            : default
        : default;

    public string? WorkspaceName =>
        JsonElement.TryGetProperty("workspace_name", out JsonElement elem) ?
            elem.GetString()
        : default;
}
