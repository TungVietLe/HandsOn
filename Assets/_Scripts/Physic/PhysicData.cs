using System.Collections.Generic;
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
        get { return rb.mass * Physics.gravity.y; }
    }
    public float PotentialEnergy
    {
        get { return Height * GravitationalForce; }
    }
    public float KineticEnergy
    {
        get { return 0.5f * Mass * Mathf.Pow(rb.velocity.y,2); }
    }
    private Rigidbody rb;
    private Dictionary<string, Vector3> forces = new();
    public void AddForce(Vector3 forceToAdd, string id)
    {
        if (forces.ContainsKey(id))
        {
            forces[id] = forceToAdd;
        }
        else
        {
            forces.Add(id, forceToAdd);
        }
    }




    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = Volume * Density;
        rb.drag = EnvironmentConfig.linearDrag;
    }

    private void FixedUpdate()
    {
        AddForce(new Vector3(0,GravitationalForce,0), "gravity");

        var net = new Vector3();
        foreach (var force in forces.Values)
        {
            net += force;
        }
        rb.AddForce(net);
    }

    [SerializeField] Material m_lineMat;

    private void OnRenderObject()
    {
        if (m_lineMat == null) return;
        m_lineMat.SetPass(0);
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);

        GL.Begin(GL.LINES);
        foreach (var force in forces.Values)
        {
            GL.Color(m_lineMat.color);
            GL.Vertex3(0, 0, 0);
            var fmultiplier = force * 0.002f;
            GL.Vertex3(fmultiplier.x, fmultiplier.y, fmultiplier.z);
        }
        GL.End();

        GL.PopMatrix();
    }
}
