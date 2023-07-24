using NotionSample.Models.Block;
using NotionSample.Models.Comment;
using NotionSample.Models.Contracts;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NotionSample;

public sealed class Program
{
    [STAThread]
    private static async Task Main(string[] _)
    {
        var notionApiKey = await Helpers.LoadApiKeyFromUserDirectory();

        if (notionApiKey == null)
        {
            await Console.Error.WriteLineAsync(
                "Cannot load API key file.");
            return;
        }

        var notion = new NotionV1Client(notionApiKey);

        var targetPageId = "56beb7b10d324c27a5c13493c92441e3";
        var page = await notion.FetchNotionPage(targetPageId);

        if (page == null)
        {
            await Console.Error.WriteLineAsync(
                $"Cannot load page object. Page ID: {targetPageId}");
            return;
        }

        await Console.Out.WriteLineAsync($"Page URL: {page.Url}");
        await Console.Out.WriteLineAsync($"Page Icon Type: {page.Icon?.Type}");

        await foreach (var eachChildBlock in notion.FetchAllChildNotionBlocks(targetPageId))
        {
            switch (eachChildBlock)
            {
                case NotionParagraphBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionBulletedListItemBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionNumberedListItemBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionCodeBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionHeadingLv1BlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionHeadingLv2BlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionHeadingLv3BlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionQuoteBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionTemplateBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionToDoBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionToggleBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {string.Concat(p.Text.Select(x => x?.PlainText))}");
                    break;

                case NotionBookmarkBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {p.Url}");
                    break;

                case NotionEmbedBlockObject p:
                    await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {p.Url}");
                    break;

                default:
                    //await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]");
                    break;
            }
        }

        return;
    }

}
