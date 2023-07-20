﻿using System.Text.Json;
using System.Collections.ObjectModel;
using NotionSample.Models.Contracts;
using NotionSample.Models.User;
using NotionSample.Models.File;
using NotionSample.Models.Emoji;
using NotionSample.Models.Parent;
using NotionSample.Models.Block;
using NotionSample.Models.RichText;
using NotionSample.Models.TemplateMention;
using NotionSample.Models.Mention;
using NotionSample.Models.PropertyValue;

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
        this JsonElement elem, params Tuple<string, Type>[] map)
    {
        var type = elem.GetProperty("type").GetString();

        for (var i = 0; i < map.Length; i++)
        {
            if (string.Equals(map[i].Item1, type, StringComparison.Ordinal))
                return Activator.CreateInstance(map[i].Item2, elem) as INotionTypedObject;
        }

        return default;
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

    public static INotionBlockObject? CreateNotionBlockObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[]
            {
                new Tuple<string, Type>("bookmark", typeof(NotionBookmarkBlockObject)),
                new Tuple<string, Type>("breadcrumb", typeof(NotionBreadcrumbBlockObject)),
                new Tuple<string, Type>("bulleted_list_item", typeof(NotionBulletedListItemBlockObject)),
                new Tuple<string, Type>("callout", typeof(NotionCalloutBlockObject)),
                new Tuple<string, Type>("child_database", typeof(NotionChildDatabaseBlockObject)),
                new Tuple<string, Type>("child_page", typeof(NotionChildPageBlockObject)),
                new Tuple<string, Type>("column", typeof(NotionColumnBlockObject)),
                new Tuple<string, Type>("column_list", typeof(NotionColumnListBlockObject)),
                new Tuple<string, Type>("divider", typeof(NotionDividerBlockObject)),
                new Tuple<string, Type>("embed", typeof(NotionEmbedBlockObject)),
                new Tuple<string, Type>("equation", typeof(NotionEquationBlockObject)),
                new Tuple<string, Type>("file", typeof(NotionFileBlockObject)),
                new Tuple<string, Type>("heading_1", typeof(NotionHeadingLv1BlockObject)),
                new Tuple<string, Type>("heading_2", typeof(NotionHeadingLv2BlockObject)),
                new Tuple<string, Type>("heading_3", typeof(NotionHeadingLv3BlockObject)),
                new Tuple<string, Type>("image", typeof(NotionImageBlockObject)),
                new Tuple<string, Type>("link_to_page", typeof(NotionLinkToPageBlockObject)),
                new Tuple<string, Type>("numbered_list_item", typeof(NotionNumberedListItemBlockObject)),
                new Tuple<string, Type>("paragraph", typeof(NotionParagraphBlockObject)),
                new Tuple<string, Type>("pdf", typeof(NotionPdfBlockObject)),
                new Tuple<string, Type>("quote", typeof(NotionQuoteBlockObject)),
                new Tuple<string, Type>("synced_block", typeof(NotionSyncedBlockObject)),
                new Tuple<string, Type>("table", typeof(NotionTableBlockObject)),
                new Tuple<string, Type>("table_of_contents", typeof(NotionTableOfContentsBlockObject)),
                new Tuple<string, Type>("table_row", typeof(NotionTableRowBlockObject)),
                new Tuple<string, Type>("template", typeof(NotionTemplateBlockObject)),
                new Tuple<string, Type>("to_do", typeof(NotionToDoBlockObject)),
                new Tuple<string, Type>("toggle", typeof(NotionToggleBlockObject)),
                new Tuple<string, Type>("unsupported", typeof(NotionUnsupportedBlockObject)),
                new Tuple<string, Type>("video", typeof(NotionVideoBlockObject)),
                new Tuple<string, Type>("database", typeof(NotionDatabaseMentionBlockObject)),
                new Tuple<string, Type>("date", typeof(NotionDateMentionBlockObject)),
                new Tuple<string, Type>("link_preview", typeof(NotionLinkPreviewMentionBlockObject)),
                new Tuple<string, Type>("page", typeof(NotionPageMentionBlockObject)),
                new Tuple<string, Type>("user", typeof(NotionUserMentionBlockObject)),
            }) as INotionBlockObject;

    public static INotionPropertyValueObject? CreateNotionPropertyValueObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[]
            {
                new Tuple<string, Type>("date", typeof(NotionDatePropertyValueObject)),
                new Tuple<string, Type>("title", typeof(NotionTitlePropertyValueObject)),
            }) as INotionPropertyValueObject;

    public static INotionRichTextObject? CreateNotionRichTextObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[]
            {
                new Tuple<string, Type>("text", typeof(NotionTextRichTextObject)),
                new Tuple<string, Type>("mention", typeof(NotionMentionRichTextObject)),
                new Tuple<string, Type>("equation", typeof(NotionEquationRichTextObject)),
            }) as INotionRichTextObject;

    public static INotionMentionObject? CreateNotionMentionObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[]
            {
                new Tuple<string, Type>("database", typeof(NotionDatabaseMentionObject)),
                new Tuple<string, Type>("date", typeof(NotionDateMentionObject)),
                new Tuple<string, Type>("link_preview", typeof(NotionLinkPreviewMentionObject)),
                new Tuple<string, Type>("page", typeof(NotionPageMentionObject)),
                new Tuple<string, Type>("template_mention", typeof(NotionTemplateMentionObject)),
                new Tuple<string, Type>("user", typeof(NotionUserMentionObject)),
            }) as INotionMentionObject;

    public static INotionTemplateMentionObject? CreateNotionTemplateMentionObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[]
            {
                new Tuple<string, Type>("template_mention_date", typeof(NotionTemplateMentionDateObject)),
                new Tuple<string, Type>("template_mention_date", typeof(NotionTemplateMentionUserObject)),
            }) as INotionTemplateMentionObject;

    public static INotionUserObject? CreateNotionUserObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[]
            {
                new Tuple<string, Type>("person", typeof(NotionPersonUserObject)),
                new Tuple<string, Type>("bot", typeof(NotionBotUserObject)),
            }) as INotionUserObject;

    public static INotionTypedObject? CreateParentNotionObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[] {
                new Tuple<string, Type>("database_id", typeof(NotionDatabaseParentObject)),
                new Tuple<string, Type>("page_id", typeof(NotionPageParentObject)),
                new Tuple<string, Type>("workspace", typeof(NotionWorkspaceParentObject)),
                new Tuple<string, Type>("block_id", typeof(NotionBlockParentObject)),
            });

    public static INotionTypedObject? CreateFileExternalNotionObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[] {
                new Tuple<string, Type>("file", typeof(NotionFileObject)),
                new Tuple<string, Type>("external", typeof(NotionExternalObject)),
            });

    public static INotionTypedObject? CreateFileExternalEmojiNotionObject(
        this JsonElement elem) =>
        elem.CreateNotionTypedObjectInstance(
            new[] {
                new Tuple<string, Type>("file", typeof(NotionFileObject)),
                new Tuple<string, Type>("external", typeof(NotionExternalObject)),
                new Tuple<string, Type>("emoji", typeof(NotionEmojiObject)),
            });
}