using System.Text.Json;
using UnityEngine;
public partial class CLUHandler
{
    /*private void Start()
    {
        AnalyzeConversation("set gravity to 10");
        AnalyzeConversation("turn on gizmos");
        AnalyzeConversation("spawn a oil container, wood, plastic, aluminum weight, and an mercury container");
    }*/
    private bool shouldRunAdjust = false;
    private JsonElement adjustPredictionToRun = new();
    private void HandleAdjust(JsonElement conversationPrediction)
    {
        foreach(JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
        {
            var eCatergory = entity.GetProperty("category").GetString();

            TryGetExtraInfo(entity, out string listKey);
            switch (eCatergory)
            {
                case "Environment.Gravity":
                    if (listKey != null) EnvironmentConfig.gravity = new Vector3(0,float.Parse(listKey),0);
                    else EnvironmentConfig.gravity = new Vector3(0, -float.Parse(entity.GetProperty("text").GetString()),0);
                    break;
                case "Environment.Gizmoz":
                    if (listKey == "true") EnvironmentConfig.ShowGizmoz = true;
                    else if (listKey == "false") EnvironmentConfig.ShowGizmoz = false;
                    else if (listKey == "toggle") EnvironmentConfig.ShowGizmoz = !EnvironmentConfig.ShowGizmoz;
                    break;
                default:
                    // code block
                    break;
            }

        }
    }
}
