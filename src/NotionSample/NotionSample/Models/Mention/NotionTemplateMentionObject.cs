using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Mention;

public sealed class NotionTemplateMentionObject : INotionMentionObject
{
    public NotionTemplateMentionObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public INotionTemplateMentionObject? TemplateMention =>
        JsonElement.TryGetProperty("template_mention", out JsonElement elem) ?
            elem.CreateNotionTemplateMentionObject()
        : default;
}
