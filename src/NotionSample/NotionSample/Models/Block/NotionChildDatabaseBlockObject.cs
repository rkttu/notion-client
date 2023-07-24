using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionChildDatabaseBlockObject : INotionBlockObject
{
    public NotionChildDatabaseBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? Title =>
        JsonElement.TryGetProperty("child_database", out JsonElement elem) ?
            elem.TryGetProperty("title", out JsonElement elem2) ?
                elem2.GetString() : default
            : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
