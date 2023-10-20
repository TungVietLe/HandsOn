using Azure.AI.Language.Conversations;
using Azure;
using System;
using UnityEngine;
using Azure.Core;
using System.Text.Json;

public class CLUHandler:MonoBehaviour
{
    static Uri endpoint = new Uri("https://handson-language.cognitiveservices.azure.com/");
    static AzureKeyCredential credential = new AzureKeyCredential("39b68f779455491da41fe178fbda077f");

    ConversationAnalysisClient client = new ConversationAnalysisClient(endpoint, credential);

    private void Start()
    {
        //AnalyzeConversation();
    }

    void AnalyzeConversation()
    {
        string projectName = "HandsOn-project";
        string deploymentName = "deploy1";

        var data = new
        {
            analysisInput = new
            {
                conversationItem = new
                {
                    text = "Give me a cup to measure",
                    id = "1",
                    participantId = "1",
                }
            },
            parameters = new
            {
                projectName,
                deploymentName,

                // Use Utf16CodeUnit for strings in .NET.
                stringIndexType = "Utf16CodeUnit",
            },
            kind = "Conversation",
        };

        Response response = client.AnalyzeConversation(RequestContent.Create(data));

        using JsonDocument result = JsonDocument.Parse(response.ContentStream);
        JsonElement conversationalTaskResult = result.RootElement;
        JsonElement conversationPrediction = conversationalTaskResult.GetProperty("result").GetProperty("prediction");

        Debug.Log($"Top intent: {conversationPrediction.GetProperty("topIntent").GetString()}");

        Debug.Log("Intents:");
        foreach (JsonElement intent in conversationPrediction.GetProperty("intents").EnumerateArray())
        {
            Debug.Log($"Category: {intent.GetProperty("category").GetString()}");
            Debug.Log($"Confidence: {intent.GetProperty("confidenceScore").GetSingle()}");
            Debug.Log("");
        }

        Debug.Log("Entities:");
        foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
        {
            Debug.Log($"Category: {entity.GetProperty("category").GetString()}");
            Debug.Log($"Text: {entity.GetProperty("text").GetString()}");
            Debug.Log($"Offset: {entity.GetProperty("offset").GetInt32()}");
            Debug.Log($"Length: {entity.GetProperty("length").GetInt32()}");
            Debug.Log($"Confidence: {entity.GetProperty("confidenceScore").GetSingle()}");
            Debug.Log("");

            if (entity.TryGetProperty("resolutions", out JsonElement resolutions))
            {
                foreach (JsonElement resolution in resolutions.EnumerateArray())
                {
                    if (resolution.GetProperty("resolutionKind").GetString() == "DateTimeResolution")
                    {
                        Debug.Log($"Datetime Sub Kind: {resolution.GetProperty("dateTimeSubKind").GetString()}");
                        Debug.Log($"Timex: {resolution.GetProperty("timex").GetString()}");
                        Debug.Log($"Value: {resolution.GetProperty("value").GetString()}");
                        Debug.Log("");
                    }
                }
            }
        }
    }
}
