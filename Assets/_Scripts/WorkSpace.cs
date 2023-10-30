using UnityEngine;

public class WorkSpace : MonoBehaviour
{
    public float Height;
    public float Width;

    [SerializeField] Transform m_tableQuad;
    private void Start()
    {
        m_tableQuad.localScale = new Vector2(Width, Height);
        CLUHandler.WorkSpaceParent = transform;
    }
}
