using UnityEngine;

public class Liquid : MonoBehaviour
{
    [HideInInspector]
    public PhysicData PhysicData;
    public int Density;
    private float m_originalVolume;
    private float m_bottomArea;

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    private void Start()
    {
        m_bottomArea = transform.lossyScale.x * transform.lossyScale.z;
        m_originalVolume = m_bottomArea * transform.lossyScale.y;
        PhysicHandler.Instance.LiquidObjects.Add(this);
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
