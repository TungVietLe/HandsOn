using System.Text.Json;
using UnityEngine;

public partial class CLUHandler
{
    [SerializeField] PhysicData m_test;
    private void Start()
    {
            m_test.GetType().GetField("Density").SetValue(m_test, 555);
    }
    private void HandleSpawn(JsonElement conversationPrediction)
    {
        foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
        {
            var fieldName = entity.GetProperty("category").GetString();
            var resource = Resources.Load($"{fieldName}/{entity.GetProperty("text").GetString()}");
            logContent.GetType().GetField(fieldName).SetValue(logContent, resource);
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
