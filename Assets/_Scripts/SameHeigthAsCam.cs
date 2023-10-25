using UnityEngine;

public class SameHeigthAsCam : MonoBehaviour
{
    private Transform camT;
    private void Start()
    {
        camT = Camera.main.transform;
    }
    private void Update()
    {
        var p = transform.position;
        transform.position = new Vector3(p.x, camT.position.y, p.z);
    }
}
