using Azure.Search.Documents;
using Azure;
using System;
using UnityEngine;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

public class SearchHandler : MonoBehaviour
{
    private void CallThisShit()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");//E:\Unity Editors Location 2\2022.3.11f1\Editor
        IConfigurationRoot configuration = builder.Build();

        SearchIndexClient indexClient = CreateSearchIndexClient(configuration);

        string indexName = configuration["SearchIndexName"];

        SearchClient indexClientForQueries = CreateSearchClientForQueries(indexName, configuration);

        RunQueries(indexClientForQueries);
    }
    private static SearchIndexClient CreateSearchIndexClient(IConfigurationRoot configuration)
    {
        string searchServiceEndPoint = configuration["SearchServiceEndPoint"];
        string adminApiKey = configuration["SearchServiceAdminApiKey"];

        SearchIndexClient indexClient = new SearchIndexClient(new Uri(searchServiceEndPoint), new AzureKeyCredential(adminApiKey));
        return indexClient;
    }
    private static SearchClient CreateSearchClientForQueries(string indexName, IConfigurationRoot configuration)
    {
        string searchServiceEndPoint = configuration["SearchServiceEndPoint"];
        string queryApiKey = configuration["SearchServiceQueryApiKey"];

        SearchClient searchClient = new SearchClient(new Uri(searchServiceEndPoint), indexName, new AzureKeyCredential(queryApiKey));
        return searchClient;
    }

    private static void DeleteIndexIfExists(string indexName, SearchIndexClient indexClient)
    {
        try
        {
            if (indexClient.GetIndex(indexName) != null)
            {
                indexClient.DeleteIndex(indexName);
            }
        }
        catch (RequestFailedException e) when (e.Status == 404)
        {
            // Throw an exception if the index name isn't found
            Debug.Log("The index doesn't exist. No deletion occurred.");
        }
    }
    private static void RunQueries(SearchClient searchClient)
    {
        SearchOptions options;
        SearchResults<Hotel> results;

        Debug.Log("Query 1: Search for 'motel'. Return only the HotelName in results:\n");

        options = new SearchOptions();
        options.Select.Add("HotelName");

        results = searchClient.Search<Hotel>("motel", options);

        WriteDocuments(results);

        Console.Write("Query 2: Apply a filter to find hotels with rooms cheaper than $100 per night, ");
        Debug.Log("returning the HotelId and Description:\n");

        options = new SearchOptions()
        {
            Filter = "Rooms/any(r: r/BaseRate lt 100)"
        };
        options.Select.Add("HotelId");
        options.Select.Add("Description");

        results = searchClient.Search<Hotel>("*", options);

        WriteDocuments(results);

        Console.Write("Query 3: Search the entire index, order by a specific field (lastRenovationDate) ");
        Console.Write("in descending order, take the top two results, and show only hotelName and ");
        Debug.Log("lastRenovationDate:\n");

        options =
            new SearchOptions()
            {
                Size = 2
            };
        options.OrderBy.Add("LastRenovationDate desc");
        options.Select.Add("HotelName");
        options.Select.Add("LastRenovationDate");

        results = searchClient.Search<Hotel>("*", options);

        WriteDocuments(results);

        Debug.Log("Query 4: Search the HotelName field for the term 'hotel':\n");

        options = new SearchOptions();
        options.SearchFields.Add("HotelName");

        //Adding details to select, because "Location" isn't supported yet when deserializing search result to "Hotel"
        options.Select.Add("HotelId");
        options.Select.Add("HotelName");
        options.Select.Add("Description");
        options.Select.Add("Category");
        options.Select.Add("Tags");
        options.Select.Add("ParkingIncluded");
        options.Select.Add("LastRenovationDate");
        options.Select.Add("Rating");
        options.Select.Add("Address");
        options.Select.Add("Rooms");

        results = searchClient.Search<Hotel>("hotel", options);

        WriteDocuments(results);
    }


    // Use-case: <Hotel> in a field definition
    static FieldBuilder fieldBuilder = new FieldBuilder();
    static System.Collections.Generic.IList<SearchField> searchFields = fieldBuilder.Build(typeof(Hotel));
    // Use-case: <Hotel> in a response
    private static void WriteDocuments(SearchResults<Hotel> searchResults)
    {
        foreach (SearchResult<Hotel> result in searchResults.GetResults())
        {
            Debug.Log(result.Document.HotelName);
        }
    }
}

public partial class Tool
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string name { get; set; }

    [SearchableField(IsSortable = true)]
    public string description { get; set; }
}


public partial class Hotel
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string HotelId { get; set; }

    [SearchableField(IsSortable = true)]
    public string HotelName { get; set; }

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public string Description { get; set; }

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.FrLucene)]
    [JsonPropertyName("Description_fr")]
    public string DescriptionFr { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public string Category { get; set; }

    [SearchableField(IsFilterable = true, IsFacetable = true)]
    public string[] Tags { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public bool? ParkingIncluded { get; set; }

    // SmokingAllowed reflects whether any room in the hotel allows smoking.
    // The JsonIgnore attribute indicates that a field should not be created 
    // in the index for this property and it will only be used by code in the client.

    [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public DateTimeOffset? LastRenovationDate { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public double? Rating { get; set; }
}