using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionObject : IJsonWrappedObject
{
    public string? Object =>
        JsonElement.TryGetProperty("object", out JsonElement elem) ?
        elem.GetString() : default;

    public Guid? Id =>
        JsonElement.TryGetProperty("id", out JsonElement elem) ?
            elem.TryGetGuid(out Guid val) ? val : default :
        default;
}
