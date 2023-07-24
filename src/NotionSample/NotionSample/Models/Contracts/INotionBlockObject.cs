using System.Text.Json;
using NotionSample.Models.User;

namespace NotionSample.Models.Contracts;

public interface INotionBlockObject : INotionObject, INotionTypedObject
{
    public INotionTypedObject? Parent =>
        JsonElement.TryGetProperty("parent", out JsonElement elem) ?
            elem.CreateParentNotionObject()
        : default;

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

    public bool? Archived =>
        JsonElement.TryGetProperty("archived", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public bool? HasChildren =>
        JsonElement.TryGetProperty("has_children", out JsonElement elem) ?
            elem.GetBoolean()
        : default;

    public IList<INotionBlockObject?> Children { get; }
}
