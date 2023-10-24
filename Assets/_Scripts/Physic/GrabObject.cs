using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class GrabObject : MonoBehaviour
{
    [SerializeField] Image m_circle;
    [SerializeField] Image m_aim;
    private bool isHolding = false;
    private Transform objectBeingGrabbed = null;
    private Camera cam;
    private string orginalTag;
    private float distanceOnPickUp;
    public void StartHold()
    {
        isHolding= true;
        m_circle.DOFade(.7f, 0.5f);
        m_aim.DOFade(.7f, 0.5f);
    }
    public void StopHold()
    {
        isHolding= false;
        m_circle.DOFade(0, 0.5f);
        m_aim.DOFade(0, 0.5f);
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
                if (hit.collider != null && !hit.collider.isTrigger)
                {
                    orginalTag = hit.transform.tag;
                    distanceOnPickUp = hit.distance;
                    hit.transform.tag = "Hold";
                    objectBeingGrabbed = hit.transform;
                    m_aim.DOKill();
                    m_aim.DOFade(0, 0.1f);
                }
            }
        }
        else if (objectBeingGrabbed != null)
        {
            objectBeingGrabbed.tag = orginalTag;
            objectBeingGrabbed = null;
            orginalTag = null;
            distanceOnPickUp = 0;
        }


        if (objectBeingGrabbed != null)
        {
            objectBeingGrabbed.position = cam.transform.position + cam.transform.forward * distanceOnPickUp;
        }
    }
}
