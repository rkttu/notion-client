using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.TemplateMention;

public sealed class NotionUnsupportedTemplateMentionObject : INotionTemplateMentionObject
{
    public NotionUnsupportedTemplateMentionObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }
}
