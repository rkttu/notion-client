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

        /*
        var targetDatabaseId = "34f08ed3ba3d4ea5aaea28a4a9876415";
        var database = await notion.RetrieveNotionDatabase(targetDatabaseId);

        if (database == null)
        {
            await Console.Error.WriteLineAsync(
                $"Cannot load database object. Page ID: {targetDatabaseId}");
            return;
        }

        await Console.Out.WriteLineAsync($"Database URL: {database.Url}");
        await Console.Out.WriteLineAsync($"Database Icon Type: {database.Icon?.Type}");
        */

        var targetPageId = "56beb7b10d324c27a5c13493c92441e3";
        var page = await notion.RetrieveNotionPage(targetPageId);

        if (page == null)
        {
            await Console.Error.WriteLineAsync(
                $"Cannot load page object. Page ID: {targetPageId}");
            return;
        }

        await Console.Out.WriteLineAsync($"Page URL: {page.Url}");
        await Console.Out.WriteLineAsync($"Page Icon Type: {page.Icon?.Type}");

        var pageBlock = await notion.RetrieveNotionBlock(targetPageId);

        if (pageBlock == null)
        {
            await Console.Error.WriteLineAsync(
                $"Cannot load page block object. Page ID: {targetPageId}");
            return;
        }

        await notion.FetchAllChildNotionBlocks(pageBlock, async (child, token) =>
        {
            if (child == null)
                return;

            await Console.Out.WriteLineAsync($"[{child?.GetType().Name}]");
        });
    }
}
