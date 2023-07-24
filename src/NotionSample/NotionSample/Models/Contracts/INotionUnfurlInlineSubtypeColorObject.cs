using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUnfurlInlineSubtypeColorObject : IJsonWrappedObject
{
    public int? RedColorValue =>
        JsonElement.TryGetProperty("color", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.TryGetProperty("r", out JsonElement elem3) ?
                    elem3.TryGetInt32(out int val) ? val : default
                : default
            : default
        : default;

    public int? GreenColorValue =>
        JsonElement.TryGetProperty("color", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.TryGetProperty("g", out JsonElement elem3) ?
                    elem3.TryGetInt32(out int val) ? val : default
                : default
            : default
        : default;

    public int? BlueColorValue =>
        JsonElement.TryGetProperty("color", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.TryGetProperty("b", out JsonElement elem3) ?
                    elem3.TryGetInt32(out int val) ? val : default
                : default
            : default
        : default;

    public string? Section =>
        JsonElement.TryGetProperty("color", out JsonElement elem) ?
            elem.TryGetProperty("section", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
