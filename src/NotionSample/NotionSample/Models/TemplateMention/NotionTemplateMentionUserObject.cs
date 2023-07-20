using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.TemplateMention;

public sealed class NotionTemplateMentionUserObject : INotionTemplateMentionObject
{
    public NotionTemplateMentionUserObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public string? TemplateMentionUser =>
        JsonElement.TryGetProperty("template_mention_user", out JsonElement elem) ?
            elem.GetString()
        : default;
}
