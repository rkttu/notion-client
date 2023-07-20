using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionTypedObject : IJsonWrappedObject
{
    public string? Type =>
        JsonElement.TryGetProperty("type", out JsonElement elem) ?
            elem.GetString()
        : default;
}
