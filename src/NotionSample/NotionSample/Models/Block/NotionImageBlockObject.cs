using System.Text.Json;
using NotionSample.Models.Attachments;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionImageBlockObject : INotionBlockObject
{
    public NotionImageBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionFileObject? Image =>
        JsonElement.TryGetProperty("image", out JsonElement elem) ?
            new NotionFileObject(elem) : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
