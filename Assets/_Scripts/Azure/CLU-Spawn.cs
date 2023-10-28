using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Schema;
using UnityEngine;

public partial class CLUHandler
{
    private void Start()
    {
        AnalyzeConversation("spawn a water weight, an iron weight, and an wood weight");
    }
    private void HandleSpawn(JsonElement conversationPrediction)
    {
        List<string> totalObjectNames = new();
        List<string> totalObjectMaterials = new();
        foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
        {
            var fieldName = entity.GetProperty("category").GetString();
            var resource = Resources.Load($"{fieldName}/{entity.GetProperty("text").GetString()}");

            switch (fieldName)
            {
                case "Object.Name":
                    totalObjectNames.Add(entity.GetProperty("text").GetString());
                    break;
                case "Object.Material":
                    totalObjectMaterials.Add(entity.GetProperty("text").GetString());
                    break;
                default:
                    // code block
                    break;
            }


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


        for (int i=0; i<totalObjectMaterials.Count; i++)
        {
            GameObject newObj;
            if (totalObjectNames.Count <= i)
            {
                var model = Resources.Load($"Object.Name/weight");
                newObj = (GameObject)Instantiate(model);
            }
            else
            {
                var model = Resources.Load($"Object.Name/{totalObjectNames[i]}");
                newObj = (GameObject) Instantiate(model);
            }
            if (newObj.TryGetComponent(out Solid solid))
            {
                solid.setMaterial(totalObjectMaterials[i]);
            }
        }
    }
}
