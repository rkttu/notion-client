using System.Text.Json;
using NotionSample.Models.File;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionPdfBlockObject : INotionBlockObject
{
    public NotionPdfBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> Caption =>
        JsonElement.TryGetProperty("pdf", out JsonElement elem) ?
            elem.TryGetProperty("caption", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public NotionFileObject? File =>
        JsonElement.TryGetProperty("pdf", out JsonElement elem) ?
            new NotionFileObject(elem) : default;

    public NotionExternalObject? External =>
        JsonElement.TryGetProperty("pdf", out JsonElement elem) ?
            new NotionExternalObject(elem) : default;
}
