using Azure.Search.Documents;
using Azure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Configuration;

public class SearchHandler : MonoBehaviour
{
    private static SearchClient CreateSearchClientForQueries(string indexName, IConfigurationRoot configuration)
    {
        string searchServiceEndPoint = configuration["SearchServiceEndPoint"];
        string queryApiKey = configuration["SearchServiceQueryApiKey"];

        SearchClient searchClient = new SearchClient(new Uri(searchServiceEndPoint), indexName, new AzureKeyCredential(queryApiKey));
        return searchClient;
    }
}
