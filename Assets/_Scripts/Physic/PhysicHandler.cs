using System.Collections.Generic;
using UnityEngine;

public class PhysicHandler : MonoBehaviour
{
    [SerializeField]
    private List<PhysicData> physicObjects;
    [SerializeField]
    private List<FluidData> fluidObjects;
    private void FixedUpdate()
    {
        foreach (PhysicData obj in physicObjects)
        {
            if (obj.CompareTag("Hold")) continue;
            Vector3 netForce = new (0, obj.GravitationalForce,0);
            foreach (FluidData fluid in fluidObjects) 
            {
                var submergeVol = CalculateIntersectionVolume(obj.transform, fluid.transform);
                Vector3 archimedesForce = new (0, -submergeVol * fluid.Density * Physics.gravity.y,0);
                netForce += archimedesForce;
                fluid.AddSumergeVolume(submergeVol);
            }

            obj.setNetForce(netForce);
        }

        foreach (FluidData fluid in fluidObjects)
        {
            fluid.UpdateFluidHeight();
        }
    }

    float CalculateIntersectionVolume(Transform t1, Transform t2)
    {
        var pos1 = t1.position;
        var pos2 = t2.position;
        var size1 = t1.lossyScale;
        var size2 = t2.lossyScale;
        Vector3 min = Vector3.Max(pos1 - size1 / 2, pos2 - size2 / 2);
        Vector3 max = Vector3.Min(pos1 + size1 / 2, pos2 + size2 / 2);

        // Check if there is an intersection
        if (min.x < max.x && min.y < max.y && min.z < max.z)
        {
            // Calculate the intersection volume
            Vector3 intersectionSize = max - min;
            float volume = intersectionSize.x * intersectionSize.y * intersectionSize.z;
            return volume;
        }

        return 0f; // No intersection
    }
}
