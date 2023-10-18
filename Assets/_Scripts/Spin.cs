using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] Vector3 rotateSpeed;

    private void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime);
    }
}
