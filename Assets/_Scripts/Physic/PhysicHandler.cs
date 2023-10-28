using System.Collections.Generic;
using UnityEngine;

public class PhysicHandler : MonoBehaviour
{
    public static PhysicHandler Instance { get; private set; }  
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
    
    [HideInInspector]public List<Solid> SolidObjects = new();
    [HideInInspector]public List<Liquid> LiquidObjects = new();
    private void FixedUpdate()
    {
        foreach (Liquid fluid in LiquidObjects) 
        {
            foreach(var obj in SolidObjects)
            {
                if (obj.CompareTag("Hold")) continue;
                var submergeVol = CalculateIntersectionVolume(obj.transform, fluid.transform);
                if (submergeVol == 0f) continue;
                Vector3 archimedesForce = new (0, -submergeVol * fluid.Density * Physics.gravity.y,0);
                fluid.AddSumergeVolume(submergeVol);
                obj.AddForce(archimedesForce, "archimedes");
            }
        fluid.UpdateFluidHeight();
        }
    }

    float CalculateIntersectionVolume(Transform t1, Transform t2)
    {
        Vector3 cube1Position = t1.localPosition;
        Vector3 cube2Position = t2.localPosition;
        Vector3 cube1Size = t1.localScale;
        Vector3 cube2Size = t2.localScale;

        // Calculate the intersection area in each plane
        float intersectionX = Mathf.Max(0, Mathf.Min(cube1Position.x + cube1Size.x / 2, cube2Position.x + cube2Size.x / 2) - Mathf.Max(cube1Position.x - cube1Size.x / 2, cube2Position.x - cube2Size.x / 2));
        float intersectionY = Mathf.Max(0, Mathf.Min(cube1Position.y + cube1Size.y / 2, cube2Position.y + cube2Size.y / 2) - Mathf.Max(cube1Position.y - cube1Size.y / 2, cube2Position.y - cube2Size.y / 2));
        float intersectionZ = Mathf.Max(0, Mathf.Min(cube1Position.z + cube1Size.z / 2, cube2Position.z + cube2Size.z / 2) - Mathf.Max(cube1Position.z - cube1Size.z / 2, cube2Position.z - cube2Size.z / 2));

        // Calculate the intersection volume
        float volume = intersectionX * intersectionY * intersectionZ;

        return volume;
    }
}
