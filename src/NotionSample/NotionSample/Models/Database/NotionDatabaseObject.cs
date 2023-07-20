using System.Text.Json;
using NotionSample.Models.Contracts;
using NotionSample.Models.User;

namespace NotionSample.Models.Page;

public sealed class NotionDatabaseObject : INotionObject
{
    public NotionDatabaseObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public DateTimeOffset? CreatedTime =>
        JsonElement.TryGetProperty("created_time", out JsonElement elem) ?
            elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default :
        default;

    public NotionPartialUserObject? CreatedBy =>
        JsonElement.TryGetProperty("created_by", out JsonElement elem) ?
            new NotionPartialUserObject(elem)
        : default;

    public DateTimeOffset? LastEditedTime =>
        JsonElement.TryGetProperty("last_edited_time", out JsonElement elem) ?
            elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default :
        default;

    public NotionPartialUserObject? LastEditedBy =>
        JsonElement.TryGetProperty("last_edited_by", out JsonElement elem) ?
            new NotionPartialUserObject(elem)
        : default;

    public IEnumerable<INotionRichTextObject?> Title =>
        JsonElement.TryGetProperty("title", out JsonElement elem) ?
            elem.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>();

    public IEnumerable<INotionRichTextObject?> Description =>
        JsonElement.TryGetProperty("description", out JsonElement elem) ?
            elem.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>();

    public INotionTypedObject? Icon =>
        JsonElement.TryGetProperty("icon", out JsonElement elem) ?
            elem.CreateFileExternalEmojiNotionObject()
        : default;

    public INotionTypedObject? Cover =>
        JsonElement.TryGetProperty("cover", out JsonElement elem) ?
            elem.CreateFileExternalNotionObject()
        : default;

    public INotionTypedObject? Parent =>
        JsonElement.TryGetProperty("parent", out JsonElement elem) ?
            elem.CreateParentNotionObject()
        : default;

    public Uri? Url =>
        JsonElement.TryGetProperty("url", out JsonElement elem) ?
            Uri.TryCreate(elem.GetString(), UriKind.Absolute, out Uri? val) ? val : default
        : default;

    public bool? Archived =>
        JsonElement.TryGetProperty("archived", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public bool? IsInline =>
        JsonElement.TryGetProperty("is_inline", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public Uri? PublicUrl =>
        JsonElement.TryGetProperty("public_url", out JsonElement elem) ?
            Uri.TryCreate(elem.GetString(), UriKind.Absolute, out Uri? val) ? val : default
        : default;

    public IDictionary<string, INotionPropertyValueObject?> Properties =>
        JsonElement.TryGetProperty("properties", out JsonElement elem) ?
            elem.CreateNotionPropertiesMap()
        : Enumerable.Empty<KeyValuePair<string, INotionPropertyValueObject?>>().ToDictionary(
            x => x.Key, x => x.Value);
}
