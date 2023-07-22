using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionRichTextPropertyValueObject : INotionPropertyValueObject
{
    public NotionRichTextPropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> Title =>
        JsonElement.TryGetProperty("rich_text", out JsonElement elem) ?
            elem.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>();
}
