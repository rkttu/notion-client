using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.RichText;

public sealed class NotionAnnotationObject : IJsonWrappedObject
{
    public NotionAnnotationObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public bool? Bold =>
        JsonElement.TryGetProperty("bold", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public bool? Italic =>
        JsonElement.TryGetProperty("italic", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public bool? StrikeThrough =>
        JsonElement.TryGetProperty("strikethrough", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public bool? Underline =>
        JsonElement.TryGetProperty("underline", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public bool? Code =>
        JsonElement.TryGetProperty("code", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public string? Color =>
        JsonElement.TryGetProperty("color", out JsonElement elem) ?
            elem.GetString()
        : default;
}
