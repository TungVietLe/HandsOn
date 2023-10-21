using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GrabObject : MonoBehaviour
{
    [SerializeField] Button m_handBtn;
    private bool isHolding = false;
    private Transform objectBeingGrabbed = null;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        m_handBtn.onClick.AddListener(() => isHolding = true);
    }

    private void Update()
    {
        if (isHolding)
        {
            if (objectBeingGrabbed == null) 
            {
                Ray ray = new()
                {
                    origin = cam.transform.position,
                    direction = cam.transform.forward
                };
                Physics.Raycast(ray, out RaycastHit hit);
                if (!hit.collider.isTrigger) objectBeingGrabbed = hit.transform;
            }
        }
        else
        {
            objectBeingGrabbed = null;
        }


        if (objectBeingGrabbed != null)
        {
            objectBeingGrabbed.position = cam.transform.position + cam.transform.forward *3;
        }
    }
}
