using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.User;

public sealed class NotionPersonUserObject : INotionUserObject
{
    public NotionPersonUserObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public Uri? PersonEmail =>
        JsonElement.TryGetProperty("person", out JsonElement elem) ?
            elem.TryGetProperty("email", out JsonElement elem2) ?
                Uri.TryCreate($"mailto:{elem2.GetString()}", UriKind.Absolute, out Uri? val) ? val : default
            : default
        : default;
}
