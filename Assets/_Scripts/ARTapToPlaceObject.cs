using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    [SerializeField] ARSession m_Session;
    [SerializeField] TextMeshProUGUI m_tmp;

    IEnumerator Start()
    {
        if ((ARSession.state == ARSessionState.None) ||
            (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
            m_tmp.text = "checking";
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            m_tmp.text = "unsupported";
        }
        else
        {
            m_tmp.text = "supported";
            m_Session.enabled = true;
        }
    }

    [SerializeField] Camera cam;
    [SerializeField] GameObject objectToPlace;
    [SerializeField] GameObject placementIndicator;

    [SerializeField] ARRaycastManager arManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("tapped");
            m_tmp.text = "tapped";
            PlaceObject();
        }
        m_tmp.text = placementPose.position.x.ToString();
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        Instantiate(objectToPlace, cam.transform.position, Quaternion.identity);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            //placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            //placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = cam.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}