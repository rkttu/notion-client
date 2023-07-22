using System;
using NotionSample.Models.Contracts;
using NotionSample.Models.User;
using System.Text.Json;

namespace NotionSample.Models.Comment
{
	public sealed class NotionCommentObject : IJsonWrappedObject
	{
        public NotionCommentObject(JsonElement elem)
        {
            JsonElement = elem;
        }

        public JsonElement JsonElement { get; private set; }

        public INotionTypedObject? Parent =>
           JsonElement.TryGetProperty("parent", out JsonElement elem) ?
               elem.CreateParentNotionObject()
           : default;

        public Guid? DiscussionId =>
            JsonElement.TryGetProperty("discussion_id", out JsonElement elem) ?
                elem.TryGetGuid(out Guid val) ? val : default
            : default;

        public DateTimeOffset? CreatedTime =>
            JsonElement.TryGetProperty("created_time", out JsonElement elem) ?
                elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default :
            default;

        public DateTimeOffset? LastEditedTime =>
            JsonElement.TryGetProperty("last_edited_time", out JsonElement elem) ?
                elem.TryGetDateTimeOffset(out DateTimeOffset val) ? val : default :
            default;

        public NotionPartialUserObject? CreatedBy =>
            JsonElement.TryGetProperty("created_by", out JsonElement elem) ?
                new NotionPartialUserObject(elem)
            : default;

        public IEnumerable<INotionRichTextObject?> RichText =>
            JsonElement.TryGetProperty("rich_text", out JsonElement elem) ?
                elem.ToNotionRichTextObjects() : Enumerable.Empty<INotionRichTextObject?>();
    }
}
