using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.File;

public sealed class NotionExternalObject : INotionTypedObject
{
    public NotionExternalObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Uri? ExternalUrl =>
        JsonElement.TryGetProperty("external", out JsonElement elem) ?
            elem.TryGetProperty("url", out JsonElement elem2) ?
                Uri.TryCreate(elem2.GetString(), UriKind.Absolute, out Uri? val) ? val : default
            : default
        : default;
}
