using System.Text.Json;
using Unity.VisualScripting;
using UnityEngine;

public partial class CLUHandler
{
    [SerializeField] PhysicData m_test;

    private GameObject nearestSpawn;
    private void Start()
    {
        AnalyzeConversation("give me a weight made of water");
    }
    private void HandleSpawn(JsonElement conversationPrediction)
    {
        foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
        {
            var fieldName = entity.GetProperty("category").GetString();
            var resource = Resources.Load($"{fieldName}/{entity.GetProperty("text").GetString()}");

            switch (fieldName)
            {
                case "Object.Name":
                    nearestSpawn = (GameObject)Instantiate(resource);
                    break;
                case "Object.Material":
                    if (nearestSpawn == null)
                    {
                        nearestSpawn = (GameObject) Instantiate(Resources.Load("Object.Name/weight"));
                    }
                    if (nearestSpawn.TryGetComponent(out MeshRenderer meshRen))
                    {
                        meshRen.material = resource as Material;
                        nearestSpawn = null;
                    }
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
    }
}
