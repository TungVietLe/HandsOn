using Azure.AI.Language.Conversations;
using Azure;
using System;
using UnityEngine;
using Azure.Core;
using System.Text.Json;
using TMPro;

public class CLUHandler:MonoBehaviour
{
    public static CLUHandler Instance { get; private set; }
    [SerializeField] 
    private TextMeshProUGUI m_logTmp;
    private string logContent;

    private static Uri endpoint = new Uri("https://handson-language.cognitiveservices.azure.com/");
    private static AzureKeyCredential credential = new AzureKeyCredential("39b68f779455491da41fe178fbda077f");

    private ConversationAnalysisClient client = new ConversationAnalysisClient(endpoint, credential);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AnalyzeConversation(string prompt)
    {
        logContent = "";
        string projectName = "HandsOn-project";
        string deploymentName = "deploy1";

        var data = new
        {
            analysisInput = new
            {
                conversationItem = new
                {
                    text = prompt,
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

        logContent += ($"Top intent: {conversationPrediction.GetProperty("topIntent").GetString()}");

        Debug.Log("Intents:");
        foreach (JsonElement intent in conversationPrediction.GetProperty("intents").EnumerateArray())
        {
            logContent += ($"Category: {intent.GetProperty("category").GetString()}");
            logContent += ($"Confidence: {intent.GetProperty("confidenceScore").GetSingle()}");
            logContent += ("");
        }

        logContent += ("Entities:");
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
    private void Update()
    {
        m_logTmp.text = logContent;
        
    }
}
