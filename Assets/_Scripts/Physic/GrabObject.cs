using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Transform objectBeingGrabbed = null;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0) 
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
