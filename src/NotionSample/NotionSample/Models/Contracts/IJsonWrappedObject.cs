using System.Text.Json;

namespace NotionSample.Models.Contracts;

public interface IJsonWrappedObject
{
    JsonElement JsonElement { get; }
}
