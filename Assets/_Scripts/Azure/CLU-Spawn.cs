using System.Text.Json;
using UnityEngine;

public partial class CLUHandler
{
    private void HandleSpawn(JsonElement conversationPrediction)
    {
        Debug.Log("RunSpawnhandler");
        foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
        {
            logContent += ($"Category: {entity.GetProperty("category").GetString()}");
            logContent += ($"Text: {entity.GetProperty("text").GetString()}");
            logContent += ($"Offset: {entity.GetProperty("offset").GetInt32()}");
            logContent += ($"Length: {entity.GetProperty("length").GetInt32()}");
            logContent += ($"Confidence: {entity.GetProperty("confidenceScore").GetSingle()}");
            logContent += ("");

            if (entity.TryGetProperty("resolutions", out JsonElement resolutions))
            {
                foreach (JsonElement resolution in resolutions.EnumerateArray())
                {
                    if (resolution.GetProperty("resolutionKind").GetString() == "DateTimeResolution")
                    {
                        logContent += ($"Datetime Sub Kind: {resolution.GetProperty("dateTimeSubKind").GetString()}");
                        logContent += ($"Timex: {resolution.GetProperty("timex").GetString()}");
                        logContent += ($"Value: {resolution.GetProperty("value").GetString()}");
                        logContent += ("");
                    }
                }
            }
        }
    }
}
