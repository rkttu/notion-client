using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.TemplateMention;

public sealed class NotionTemplateMentionDateObject : INotionTemplateMentionObject
{
    public NotionTemplateMentionDateObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? TemplateMentionDate =>
        JsonElement.TryGetProperty("template_mention_date", out JsonElement elem) ?
            elem.GetString()
        : default;
}
