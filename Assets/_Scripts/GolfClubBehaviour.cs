using System.Collections;
using UnityEngine;

public class GolfClubBehaviour : MonoBehaviour
{
    [SerializeField] Transform golfBall;
    [SerializeField] Transform golfClub;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        StartCoroutine(UpdateEvery10Sec());
    }

    private void Update()
    {
        AlignPositionWithCam();
        AlignRotationWithCam();
    }
    private void MoveClubToBall()
    {
        transform.position = golfBall.transform.position;
    }
    private void AlignPositionWithCam()
    {
        Vector3 camToBall = cam.transform.position - golfBall.position;
        transform.right = new Vector3(camToBall.x, 0, camToBall.z);
    }
    private void AlignRotationWithCam()
    {
        golfClub.transform.up = -cam.transform.forward;
    }

    private IEnumerator UpdateEvery10Sec()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(10);
            MoveClubToBall();
        }
    }
}
