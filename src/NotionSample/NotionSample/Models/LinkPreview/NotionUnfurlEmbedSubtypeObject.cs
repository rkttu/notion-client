using NotionSample.Models.Contracts;
using System.Text.Json;

namespace NotionSample.Models.LinkPreview;

public sealed class NotionUnfurlEmbedSubtypeObject :
    IJsonWrappedObject,
    INotionUnfurlEmbedSubtypeAudioObject,
    INotionUnfurlEmbedSubtypeHtmlObject,
    INotionUnfurlEmbedSubtypeImageObject,
    INotionUnfurlEmbedSubtypeVideoObject
{
    public NotionUnfurlEmbedSubtypeObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public INotionUnfurlEmbedSubtypeAudioObject? ColorValue =>
        JsonElement.TryGetProperty("audio", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlEmbedSubtypeHtmlObject? DateValue =>
        JsonElement.TryGetProperty("html", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlEmbedSubtypeImageObject? DateTimeValue =>
        JsonElement.TryGetProperty("image", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlEmbedSubtypeVideoObject? EnumValue =>
        JsonElement.TryGetProperty("video", out JsonElement _) ?
            this
        : default;
}
