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
            await Console.Out.WriteLineAsync($"[{eachChildBlock?.GetType().Name}]: {eachChildBlock?.Id}");
        }
    }

}
