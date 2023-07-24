using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionEquationBlockObject : INotionBlockObject
{
    public NotionEquationBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? Expression =>
        JsonElement.TryGetProperty("equation", out JsonElement elem) ?
            elem.TryGetProperty("expression", out JsonElement elem2) ?
                elem2.GetString() : default
            : default;

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
