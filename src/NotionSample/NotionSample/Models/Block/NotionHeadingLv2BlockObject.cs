using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionHeadingLv2BlockObject : INotionBlockObject
{
    public NotionHeadingLv2BlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> RichText =>
        JsonElement.TryGetProperty("heading_2", out JsonElement elem) ?
            elem.TryGetProperty("rich_text", out JsonElement elem2) ?
                elem2.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>()
            : Enumerable.Empty<INotionRichTextObject?>();

    public string? Color =>
        JsonElement.TryGetProperty("heading_2", out JsonElement elem) ?
            elem.TryGetProperty("color", out JsonElement elem2) ?
                elem2.GetString() : default
            : default;

    public bool? IsToggleable =>
        JsonElement.TryGetProperty("heading_2", out JsonElement elem) ?
            elem.TryGetProperty("is_toggleable", out JsonElement elem2) ?
                elem2.GetBoolean() : default
            : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
