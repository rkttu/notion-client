using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.File;

public sealed class NotionFileObject : INotionTypedObject
{
    public NotionFileObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Uri? FileUrl =>
        JsonElement.TryGetProperty("file", out JsonElement elem) ?
            elem.TryGetProperty("url", out JsonElement elem2) ?
                Uri.TryCreate(elem2.GetString(), UriKind.Absolute, out Uri? val) ? val : default
            : default
        : default;

    public DateTimeOffset? FileExpiryTime =>
        JsonElement.TryGetProperty("file", out JsonElement elem) ?
            elem.TryGetProperty("expiry_time", out JsonElement elem2) ?
                elem2.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default
            : default
        : default;
}
