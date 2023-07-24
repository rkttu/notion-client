using System.Text.Json;
using System.Collections.ObjectModel;
using NotionSample.Models.Contracts;
using NotionSample.Models.User;
using NotionSample.Models.Parent;
using NotionSample.Models.Block;
using NotionSample.Models.RichText;
using NotionSample.Models.TemplateMention;
using NotionSample.Models.Mention;
using NotionSample.Models.PropertyValue;
using NotionSample.Models.Attachments;
using NotionSample.Models.LinkPreview;

namespace NotionSample.Models;

internal static class NotionWrappedObjectExtensions
{
    public static IDictionary<string, INotionPropertyValueObject?> CreateNotionPropertiesMap(
        this JsonElement elem)
    {
        var dict = new Dictionary<string, INotionPropertyValueObject?>();

        foreach (var eachObject in elem.EnumerateObject())
        {
            dict.Add(
                eachObject.Name,
                eachObject.Value.CreateNotionPropertyValueObject());
        }

        return new ReadOnlyDictionary<string, INotionPropertyValueObject?>(dict);
    }

    public static INotionTypedObject? CreateNotionTypedObjectInstance(
        this JsonElement elem,
        IDictionary<string, Type> map,
        Type defaultType)
    {
        var type = elem.GetProperty("type").GetString();

        foreach (var eachPair in map)
        {
            if (string.Equals(eachPair.Key, type, StringComparison.Ordinal))
                return Activator.CreateInstance(eachPair.Value, elem) as INotionTypedObject;
        }

        return Activator.CreateInstance(defaultType, elem) as INotionTypedObject;
    }

    public static INotionRichTextObject? CreateNotionTypedRichTextObjectInstance(
        this JsonElement elem,
        IDictionary<string, Type> map,
        Type defaultType)
    {
        var type = elem.GetProperty("type").GetString();

        foreach (var eachPair in map)
        {
            if (string.Equals(eachPair.Key, type, StringComparison.Ordinal))
                return Activator.CreateInstance(eachPair.Value, elem) as INotionRichTextObject;
        }

        return Activator.CreateInstance(defaultType, elem) as INotionRichTextObject;
    }

    public static IEnumerable<INotionRichTextObject?> ToNotionRichTextObjects(
        this JsonElement elem)
    {
        var length = elem.GetArrayLength();

        if (length < 1)
            return Enumerable.Empty<INotionRichTextObject>();

        var list = new List<INotionRichTextObject?>(length);

        for (var i = 0; i < length; i++)
            list.Add(elem[i].CreateNotionRichTextObject());

        return list.AsReadOnly();
    }

    public static IEnumerable<NotionUnfurlAttributeObject?> ToNotionUnfurlAttributeObjects(
        this JsonElement elem)
    {
        var length = elem.GetArrayLength();

        if (length < 1)
            return Enumerable.Empty<NotionUnfurlAttributeObject?>();

        var list = new List<NotionUnfurlAttributeObject?>(length);

        for (var i = 0; i < length; i++)
            list.Add(new NotionUnfurlAttributeObject(elem[i]));

        return list.AsReadOnly();
    }

    public static IEnumerable<INotionBlockObject?> ToNotionBlockObjects(
        this JsonElement elem)
    {
        var length = elem.GetArrayLength();

        if (length < 1)
            return Enumerable.Empty<INotionBlockObject>();

        var list = new List<INotionBlockObject?>(length);

        for (var i = 0; i < length; i++)
            list.Add(elem[i].CreateNotionBlockObject());

        return list.AsReadOnly();
    }

    private static readonly IDictionary<string, Type> BlockObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "bookmark", typeof(NotionBookmarkBlockObject) },
            { "breadcrumb", typeof(NotionBreadcrumbBlockObject) },
            { "bulleted_list_item", typeof(NotionBulletedListItemBlockObject) },
            { "callout", typeof(NotionCalloutBlockObject) },
            { "child_database", typeof(NotionChildDatabaseBlockObject) },
            { "child_page", typeof(NotionChildPageBlockObject) },
            { "column", typeof(NotionColumnBlockObject) },
            { "column_list", typeof(NotionColumnListBlockObject) },
            { "divider", typeof(NotionDividerBlockObject) },
            { "embed", typeof(NotionEmbedBlockObject) },
            { "equation", typeof(NotionEquationBlockObject) },
            { "file", typeof(NotionFileBlockObject) },
            { "heading_1", typeof(NotionHeadingLv1BlockObject) },
            { "heading_2", typeof(NotionHeadingLv2BlockObject) },
            { "heading_3", typeof(NotionHeadingLv3BlockObject) },
            { "image", typeof(NotionImageBlockObject) },
            { "link_to_page", typeof(NotionLinkToPageBlockObject) },
            { "numbered_list_item", typeof(NotionNumberedListItemBlockObject) },
            { "paragraph", typeof(NotionParagraphBlockObject) },
            { "pdf", typeof(NotionPdfBlockObject) },
            { "quote", typeof(NotionQuoteBlockObject) },
            { "synced_block", typeof(NotionSyncedBlockObject) },
            { "table", typeof(NotionTableBlockObject) },
            { "table_of_contents", typeof(NotionTableOfContentsBlockObject) },
            { "table_row", typeof(NotionTableRowBlockObject) },
            { "template", typeof(NotionTemplateBlockObject) },
            { "to_do", typeof(NotionToDoBlockObject) },
            { "toggle", typeof(NotionToggleBlockObject) },
            { "unsupported", typeof(NotionUnsupportedBlockObject) },
            { "video", typeof(NotionVideoBlockObject) },
            { "database", typeof(NotionDatabaseMentionBlockObject) },
            { "date", typeof(NotionDateMentionBlockObject) },
            { "link_preview", typeof(NotionLinkPreviewMentionBlockObject) },
            { "page", typeof(NotionPageMentionBlockObject) },
            { "user", typeof(NotionUserMentionBlockObject) }
        });

    public static INotionBlockObject? CreateNotionBlockObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(BlockObjectTypes, typeof(NotionUnsupportedBlockObject)) as INotionBlockObject;

    private static readonly IDictionary<string, Type> PropertyValueObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "date", typeof(NotionDatePropertyValueObject) },
            { "title", typeof(NotionTitlePropertyValueObject) },
            { "rich_text", typeof(NotionRichTextPropertyValueObject) },
            { "number", typeof(NotionNumberPropertyValueObject) },
            { "select", typeof(NotionSelectPropertyValueObject) },
            { "multi_select", typeof(NotionMultiSelectPropertyValueObject) },
            { "status", typeof(NotionStatusPropertyValueObject) },
            { "formula", typeof(NotionFormulaPropertyValueObject) },
            { "relation", typeof(NotionRelationPropertyValueObject) },
            { "rollup", typeof(NotionRollupPropertyValueObject) },
            { "people", typeof(NotionPeoplePropertyValueObject) },
            { "files", typeof(NotionFilesPropertyValueObject) },
            { "checkbox", typeof(NotionCheckBoxPropertyValueObject) },
            { "url", typeof(NotionUrlPropertyValueObject) },
            { "email", typeof(NotionEmailPropertyValueObject) },
            { "phone_number", typeof(NotionPhoneNumberPropertyValueObject) },
            { "created_time", typeof(NotionCreatedTimePropertyValueObject) },
            { "created_by", typeof(NotionCreatedByPropertyValueObject) },
            { "last_edited_time", typeof(NotionLastEditedTimePropertyValueObject) },
            { "last_edited_by", typeof(NotionLastEditedByPropertyValueObject) }
        });

    public static INotionPropertyValueObject? CreateNotionPropertyValueObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(PropertyValueObjectTypes, typeof(NotionUnsupportedPropertyValueTypeObject)) as INotionPropertyValueObject;

    private static readonly IDictionary<string, Type> RichTextObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "text", typeof(NotionTextRichTextObject) },
            { "mention", typeof(NotionMentionRichTextObject) },
            { "equation", typeof(NotionEquationRichTextObject) }
        });

    public static INotionRichTextObject? CreateNotionRichTextObject(this JsonElement elem) =>
        elem.CreateNotionTypedRichTextObjectInstance(RichTextObjectTypes, typeof(NotionUnsupportedRichTextObject));

    private static readonly IDictionary<string, Type> MentionObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "database", typeof(NotionDatabaseMentionObject) },
            { "date", typeof(NotionDateMentionObject) },
            { "link_preview", typeof(NotionLinkPreviewMentionObject) },
            { "page", typeof(NotionPageMentionObject) },
            { "template_mention", typeof(NotionTemplateMentionObject) },
            { "user", typeof(NotionUserMentionObject) }
        });

    public static INotionMentionObject? CreateNotionMentionObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(MentionObjectTypes, typeof(NotionUnsupportedMentionObject)) as INotionMentionObject;

    private static readonly IDictionary<string, Type> TemplateMentionObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "template_mention_date", typeof(NotionTemplateMentionDateObject) },
            { "template_mention_user", typeof(NotionTemplateMentionUserObject) }
        });

    public static INotionTemplateMentionObject? CreateNotionTemplateMentionObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(TemplateMentionObjectTypes, typeof(NotionUnsupportedTemplateMentionObject)) as INotionTemplateMentionObject;

    private static readonly IDictionary<string, Type> UserObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "person", typeof(NotionPersonUserObject) },
            { "bot", typeof(NotionBotUserObject) }
        });

    public static INotionUserObject? CreateNotionUserObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(UserObjectTypes, typeof(NotionUnsupportedUserObject)) as INotionUserObject;

    private static readonly IDictionary<string, Type> ParentObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "database_id", typeof(NotionDatabaseParentObject) },
            { "page_id", typeof(NotionPageParentObject) },
            { "workspace", typeof(NotionWorkspaceParentObject) },
            { "block_id", typeof(NotionBlockParentObject) }
        });

    public static INotionTypedObject? CreateParentNotionObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(ParentObjectTypes, typeof(NotionUnsupportedParentObject));

    private static readonly IDictionary<string, Type> FileExternalObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "file", typeof(NotionFileObject) },
            { "external", typeof(NotionExternalObject) }
        });

    public static INotionTypedObject? CreateFileExternalNotionObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(FileExternalObjectTypes, typeof(NotionUnsupportedAttachmentObject));

    private static readonly IDictionary<string, Type> FileExternalEmojiObjectTypes =
        new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            { "file", typeof(NotionFileObject) },
            { "external", typeof(NotionExternalObject) },
            { "emoji", typeof(NotionEmojiObject) }
        });

    public static INotionTypedObject? CreateFileExternalEmojiNotionObject(this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(FileExternalEmojiObjectTypes, typeof(NotionUnsupportedAttachmentObject));
}
