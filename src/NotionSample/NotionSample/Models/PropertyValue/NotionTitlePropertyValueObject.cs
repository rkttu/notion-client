using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.PropertyValue;

public sealed class NotionTitlePropertyValueObject : INotionPropertyValueObject
{
    public NotionTitlePropertyValueObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<INotionRichTextObject?> Title =>
        JsonElement.TryGetProperty("title", out JsonElement elem) ?
            elem.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>();
}
