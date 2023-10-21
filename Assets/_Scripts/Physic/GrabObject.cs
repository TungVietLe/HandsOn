using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class GrabObject : MonoBehaviour
{
    private bool isHolding = false;
    private Transform objectBeingGrabbed = null;
    private Camera cam;

    public void StartHold()
    {
        isHolding= true;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void StopHold()
    {
        isHolding= false;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void Start()
    {
        cam = Camera.main;
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
