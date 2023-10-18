using Azure.AI.Language.Conversations;
using Azure;
using System;

public class CLUHandler 
{
    static Uri endpoint = new Uri("https://myaccount.cognitive.microsoft.com");
    static AzureKeyCredential credential = new AzureKeyCredential("{api-key}");

    ConversationAnalysisClient client = new ConversationAnalysisClient(endpoint, credential);
}
