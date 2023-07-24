using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUnfurlInlineSubtypeEnumObject : IJsonWrappedObject
{
    public string? EnumValue =>
        JsonElement.TryGetProperty("enum", out JsonElement elem) ?
            elem.TryGetProperty("value", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;

    public int? EnumRedColor =>
        JsonElement.TryGetProperty("enum", out JsonElement elem) ?
            elem.TryGetProperty("color", out JsonElement elem2) ?
                elem2.TryGetProperty("r", out JsonElement elem3) ?
                    elem3.TryGetInt32(out int val) ? val : default
                : default
            : default
        : default;

    public int? EnumBlueColor =>
        JsonElement.TryGetProperty("enum", out JsonElement elem) ?
            elem.TryGetProperty("color", out JsonElement elem2) ?
                elem2.TryGetProperty("b", out JsonElement elem3) ?
                    elem3.TryGetInt32(out int val) ? val : default
                : default
            : default
        : default;

    public int? EnumGreenColor =>
        JsonElement.TryGetProperty("enum", out JsonElement elem) ?
            elem.TryGetProperty("color", out JsonElement elem2) ?
                elem2.TryGetProperty("g", out JsonElement elem3) ?
                    elem3.TryGetInt32(out int val) ? val : default
                : default
            : default
        : default;

    public string? Section =>
        JsonElement.TryGetProperty("enum", out JsonElement elem) ?
            elem.TryGetProperty("section", out JsonElement elem2) ?
                elem2.GetString()
            : default
        : default;
}
