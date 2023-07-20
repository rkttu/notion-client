using System.Text.Json;
using NotionSample.Models.File;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionFileBlockObject : INotionBlockObject
{
    public NotionFileBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> Caption =>
        JsonElement.TryGetProperty("file", out JsonElement elem) ?
            elem.TryGetProperty("caption", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public string? Expression =>
        JsonElement.TryGetProperty("file", out JsonElement elem) ?
            elem.TryGetProperty("type", out JsonElement elem2) ?
                elem2.GetString() : default
            : default;

    public NotionFileObject? File =>
        JsonElement.TryGetProperty("file", out JsonElement elem) ?
            elem.TryGetProperty("file", out JsonElement elem2) ?
                new NotionFileObject(elem2) : default
            : default;
}
