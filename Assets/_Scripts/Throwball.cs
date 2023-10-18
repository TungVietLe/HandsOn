using UnityEngine;

public class Throwball : MonoBehaviour
{
    [SerializeField] private Rigidbody m_objectToFire;
    [SerializeField] private float m_forceRegardlessMass;

    private bool shouldFire = false;
    private void Update()
    {
        HandleMobileInput();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (!shouldFire) return;
        Camera cam = Camera.main;
        var bullet = Instantiate(m_objectToFire, cam.transform.position, Quaternion.identity);
        bullet.AddForce(cam.transform.forward * m_forceRegardlessMass * m_objectToFire.mass, ForceMode.Impulse);
    }

    private void HandleMobileInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            shouldFire= true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldFire = true;
            return;
        }

        shouldFire = false;
    }
}
