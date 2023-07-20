using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionTableBlockObject : INotionBlockObject
{
    public NotionTableBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public int? TableWidth =>
        JsonElement.TryGetProperty("table", out JsonElement elem) ?
            elem.TryGetProperty("table_width", out JsonElement elem2) ?
                elem2.TryGetInt32(out int val) ? val : default
            : default
        : default;

    public bool? HasColumnHeader =>
        JsonElement.TryGetProperty("table", out JsonElement elem) ?
            elem.TryGetProperty("has_column_header", out JsonElement elem2) ?
                elem2.GetBoolean()
            : default
        : default;

    public bool? HasRowHeader =>
        JsonElement.TryGetProperty("table", out JsonElement elem) ?
            elem.TryGetProperty("has_row_header", out JsonElement elem2) ?
                elem2.GetBoolean()
            : default
        : default;
}
