using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUserObject : INotionObject, INotionTypedObject
{
    public string? Name =>
        JsonElement.TryGetProperty("name", out JsonElement elem) ?
            elem.GetString()
        : default;

    public Uri? AvatarUrl =>
        JsonElement.TryGetProperty("avatar_url", out JsonElement elem) ?
            Uri.TryCreate(elem.GetString(), UriKind.Absolute, out Uri? val) ? val : default
        : default;
}
