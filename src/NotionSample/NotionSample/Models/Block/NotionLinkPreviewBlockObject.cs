using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionLinkPreviewBlockObject : INotionBlockObject
{
    public NotionLinkPreviewBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Uri? LinkPreviewUrl =>
        JsonElement.TryGetProperty("link_preview", out JsonElement elem) ?
            elem.TryGetProperty("url", out JsonElement elem2) ?
                Uri.TryCreate(elem2.GetString(), UriKind.Absolute, out Uri? val) ? val : default
            : default
        : default;
}
