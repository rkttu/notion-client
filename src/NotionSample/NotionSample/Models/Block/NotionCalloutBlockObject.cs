﻿using System.Text.Json;
using NotionSample.Models.Contracts;

namespace NotionSample.Models.Block;

public sealed class NotionCalloutBlockObject : INotionBlockObject
{
    public NotionCalloutBlockObject(JsonElement elem)
    {
        JsonElement = elem;
    }

    public JsonElement JsonElement { get; private set; }

    public IList<INotionBlockObject?> Children { get; private set; } =
        new List<INotionBlockObject?>();
}
