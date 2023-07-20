using System.Text.Json;
using NotionSample.Models.File;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionVideoBlockObject : INotionBlockObject
{
    public NotionVideoBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public NotionFileObject? Image =>
        JsonElement.TryGetProperty("video", out JsonElement elem) ?
            new NotionFileObject(elem) : default;
}
