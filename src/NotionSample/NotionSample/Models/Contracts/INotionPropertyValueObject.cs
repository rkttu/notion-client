using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionPropertyValueObject : INotionTypedObject
{
    public string? Id =>
        JsonElement.TryGetProperty("id", out JsonElement elem) ?
            elem.GetString()
        : default;
}
