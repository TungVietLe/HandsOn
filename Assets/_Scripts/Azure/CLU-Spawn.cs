using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

public partial class CLUHandler
{
    /*private void Start()
    {
        AnalyzeConversation("spawn a oil container, wood, plastic, aluminum weight, and an mercury container");
    }*/
    private void HandleSpawn(JsonElement conversationPrediction)
    {
        print("launch spawn");
        List<string> totalObjectNames = new();
        List<string> totalSolidMaterials = new();
        List<string> totalLiquidMaterials = new();
        foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
        {
            var eCatergory = entity.GetProperty("category").GetString();

            TryGetExtraInfo(entity, out string listKey);
            switch (eCatergory)
            {
                case "Object.Name":
                    if (listKey == "container") break;
                    if (listKey != null) totalObjectNames.Add(listKey);
                    else totalObjectNames.Add(entity.GetProperty("text").GetString());
                    break;
                case "Object.Material":
                    if (listKey == "liquid") totalLiquidMaterials.Add(entity.GetProperty("text").GetString());
                    else totalSolidMaterials.Add(entity.GetProperty("text").GetString());
                    break;
                default:
                    // code block
                    break;
            }

        }

        print($"{totalObjectNames.Count} {totalSolidMaterials.Count} {totalLiquidMaterials.Count}");
        for (int i=0; i<totalSolidMaterials.Count; i++)
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
                solid.setMaterial(totalSolidMaterials[i]);
            }
        }

        var allContainers = FindObjectsOfType<Liquid>();
        for(int i =0; i<totalLiquidMaterials.Count;i++)
        {
            var liquidMat = totalLiquidMaterials[i];
            if (i<allContainers.Length)
            {
                allContainers[i].setMaterial(liquidMat);  
            }
            else
            {
                var newContainer = (GameObject) Instantiate(Resources.Load("Object.Name/container"));
                newContainer.GetComponentInChildren<Liquid>().setMaterial(liquidMat);
            }
        }
    }
    private void TryGetExtraInfo(JsonElement entity, out string extraInfo)
    {
        if (entity.TryGetProperty("extraInformation", out JsonElement extras))
        {
            foreach (JsonElement extra in extras.EnumerateArray())
            {
                if (extra.TryGetProperty("key", out JsonElement value))
                {
                    extraInfo= value.ToString();    
                    print("afd"+extraInfo);
                    return;
                }
            }
        }
        extraInfo = null;
    }
}
