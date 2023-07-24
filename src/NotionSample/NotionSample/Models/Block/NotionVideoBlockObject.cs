using System.Text.Json;
using NotionSample.Models.Attachments;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionVideoBlockObject : INotionBlockObject
{
    public NotionVideoBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionFileObject? Video =>
        JsonElement.TryGetProperty("video", out JsonElement elem) ?
            new NotionFileObject(elem) : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
