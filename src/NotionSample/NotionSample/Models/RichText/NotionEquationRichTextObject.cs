using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.RichText;

public sealed class NotionEquationRichTextObject : INotionRichTextObject
{
    public NotionEquationRichTextObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? EquationExpression =>
        JsonElement.TryGetProperty("equation", out JsonElement elem) ?
            elem.TryGetProperty("expression", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
