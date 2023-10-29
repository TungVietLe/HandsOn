using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

public partial class CLUHandler
{
    private void Start()
    {
        AnalyzeConversation("spawn a wood weight, an iron weight, and an mercury weight");
    }
    private void HandleSpawn(JsonElement conversationPrediction)
    {
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

        foreach(var liquidMat in totalLiquidMaterials)
        {
            GameObject.FindAnyObjectByType<Liquid>().setMaterial(liquidMat);
        }
    }
    private void TryGetExtraInfo(JsonElement entity, out string extraInfo)
    {
        if (entity.TryGetProperty("extraInformation", out JsonElement extras))
        {
            foreach (JsonElement extra in extras.EnumerateArray())
            {
                extraInfo = extra.GetProperty("key").ToString();
                return;
            }
        }
        extraInfo = null;
    }
}
