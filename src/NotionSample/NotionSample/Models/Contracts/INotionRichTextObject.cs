using System.Text.Json;
using NotionSample.Models.RichText;

namespace NotionSample.Models.Contracts;

public interface INotionRichTextObject : IJsonWrappedObject
{
    public NotionAnnotationObject? Annotations =>
        JsonElement.TryGetProperty("annotations", out JsonElement elem) ?
            new NotionAnnotationObject(elem)
        : default;

    public string? PlainText =>
        JsonElement.TryGetProperty("plain_text", out JsonElement elem) ?
            elem.GetString()
        : default;

    public Uri? Href =>
        JsonElement.TryGetProperty("href", out JsonElement elem) ?
            Uri.TryCreate(elem.GetString(), UriKind.Absolute, out Uri? val) ? val : default
        : default;
}
