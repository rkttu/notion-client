using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface INotionUnfurlEmbedSubtypeAudioObject : IJsonWrappedObject
{
    public Uri? SourceUrl =>
        JsonElement.TryGetProperty("audio", out JsonElement elem) ?
            elem.TryGetProperty("src_url", out JsonElement elem2) ?
                Uri.TryCreate(elem2.GetString(), UriKind.Absolute, out Uri? val) ? val : default
            : default
        : default;

    public string? Section =>
        JsonElement.TryGetProperty("audio", out JsonElement elem) ?
            elem.TryGetProperty("audio", out JsonElement elem2) ?
                elem2.TryGetProperty("section", out JsonElement elem3) ?
                    elem3.GetString()
                : default
            : default
        : default;
}
