using Unity.VisualScripting;
using UnityEngine;

public class PhysicData:MonoBehaviour
{
    public float Density = 1000;

    private string state;
    public string State
    {
        get { return state; }
        set
        {
            state = value;
            if (state == "liquid")
            {
                var newComponent = this.AddComponent<Liquid>();
                newComponent.PhysicData= this;
            }
            else if (state == "solid")
            {
                var newComponent = this.AddComponent<Solid>();
                newComponent.PhysicData = this;
            }
        }
    }

    public Material Material
    {
        get { return GetComponent<MeshRenderer>().material; }
        set
        {
            if (TryGetComponent<MeshRenderer>(out MeshRenderer meshRen))
            {
                meshRen.material = value;
            }
            else
            {
                meshRen = this.AddComponent<MeshRenderer>();
                meshRen.material = value;
            }
        }
    }
}
