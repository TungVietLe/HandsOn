using UnityEngine;

public class Liquid : MonoBehaviour
{
    public int Density
    {
        get { return DB.Density[m_materialID]; }
    }
    [SerializeField]
    private string m_materialID;
    private float m_originalVolume;
    private float m_bottomArea;

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    public void setMaterial(string _id)
    {
        m_materialID = _id;
        GetComponent<MeshRenderer>().material = (Material)Resources.Load($"Object.Material/{_id}");
    }
    private void Start()
    {
        PhysicHandler.Instance.LiquidObjects.Add(this);
        transform.parent = null;
        m_bottomArea = transform.lossyScale.x * transform.lossyScale.z;
        m_originalVolume = m_bottomArea * transform.lossyScale.y;
    }
    private float m_totalSubmergeVolume;
    public void AddSumergeVolume(float volumeToAdd)
    {
        m_totalSubmergeVolume += volumeToAdd;   
    }
    public void UpdateFluidHeight()
    {
        var newVolume = m_totalSubmergeVolume + m_originalVolume;
        var newHeight = (float)newVolume / m_bottomArea;
        transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);
        transform.localPosition = new Vector3(transform.localPosition.x, (float)newHeight / 2, transform.localPosition.z);
        m_totalSubmergeVolume = 0;
    }
}
