using System;
using System.Collections.Generic;
using UnityEngine;

[Obsolete()]
public class PhysicData:MonoBehaviour
{
    public Action onDataChanged;
    public float Name;
    public string State;

    private string m_materialStr;
    public string MaterialString
    {
        get { return m_materialStr; }
        set { 
            m_materialStr = value;
            onDataChanged?.Invoke();
    }
    }
    public Material Material
    {
        get { return (Material) Resources.Load($"Object.Material/{MaterialString}"); }
    }
    public int Density {
        get { return DB.Density[MaterialString]; }
    }
    public float Height
    {
        get { return transform.position.y; }
    }
    public float Volume
    {
        get { return this.transform.lossyScale.x * transform.lossyScale.y * transform.lossyScale.z; }
    }
    public float Mass
    {
        get { return rb.mass; }
    }
    public float GravitationalForce
    {
        get { return rb.mass * Physics.gravity.y; }
    }
    public Rigidbody rb;

    public void Init( Transform _transform, string _materialString="", Rigidbody _rb = null)
    {
        rb = _rb;
        MaterialString = _materialString;

        if (rb == null) return;
        rb.useGravity = false;
        rb.mass = Volume * Density;
        rb.drag = EnvironmentConfig.linearDrag;
    }
}


public class DB
{
    public static Dictionary<string, int> Density = new Dictionary<string, int>
        {
            {"wood",    680},
            {"oil",     700 },
            {"ice",     910},
            {"plastic", 940},
            {"water",   1000},
            {"milk",    1050 },
            {"coal",    1400},
            {"aluminium",2700},
            {"iron",    7800},
            {"mercury", 13500},
        };
}