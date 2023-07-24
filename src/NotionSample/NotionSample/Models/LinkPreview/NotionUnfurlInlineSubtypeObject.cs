using NotionSample.Models.Contracts;
using System.Text.Json;

namespace NotionSample.Models.LinkPreview;

public sealed class NotionUnfurlInlineSubtypeObject :
    IJsonWrappedObject,
    INotionUnfurlInlineSubtypeColorObject,
    INotionUnfurlInlineSubtypeDateObject,
    INotionUnfurlInlineSubtypeDateTimeObject,
    INotionUnfurlInlineSubtypeEnumObject,
    INotionUnfurlInlineSubtypePlainTextObject,
    INotionUnfurlInlineSubtypeTitleObject
{
    public NotionUnfurlInlineSubtypeObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public INotionUnfurlInlineSubtypeColorObject? ColorValue =>
        JsonElement.TryGetProperty("color", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlInlineSubtypeDateObject? DateValue =>
        JsonElement.TryGetProperty("date", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlInlineSubtypeDateTimeObject? DateTimeValue =>
        JsonElement.TryGetProperty("datetime", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlInlineSubtypeEnumObject? EnumValue =>
        JsonElement.TryGetProperty("enum", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlInlineSubtypePlainTextObject? PlainTextValue =>
        JsonElement.TryGetProperty("plain_text", out JsonElement _) ?
            this
        : default;

    public INotionUnfurlInlineSubtypeTitleObject? TitleValue =>
        JsonElement.TryGetProperty("title", out JsonElement _) ?
            this
        : default;
}
