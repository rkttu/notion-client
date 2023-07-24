using NotionSample.Models.Contracts;
using System.Text.Json;

namespace NotionSample.Models.LinkPreview;

public sealed class NotionUnfurlAttributeArrayObject : INotionTypedObject
{
    public NotionUnfurlAttributeArrayObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IEnumerable<NotionUnfurlAttributeObject?> Attributes =>
        JsonElement.ToNotionUnfurlAttributeObjects();
}
