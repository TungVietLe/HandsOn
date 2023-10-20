using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicData : MonoBehaviour
{
    public float Density;
    public float Height
    {
        get { return transform.position.y; }
    }
    public float Volume
    {
        get { return transform.lossyScale.x * transform.lossyScale.y * transform.lossyScale.z; }
    }
    public float Mass
    {
        get { return rb.mass; }
    }
    public float GravitationalForce
    {
        get { return Mass * Physics.gravity.y; }
    }
    public float PotentialEnergy
    {
        get { return Height * GravitationalForce; }
    }
    public float KineticEnergy
    {
        get { return 0.5f * Mass * Mathf.Pow(rb.velocity.y,2); }
    }
    public void setNetForce(Vector3 netForce)
    {
        rb.AddForce(netForce);
    }

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = Volume * Density;
    }
}
