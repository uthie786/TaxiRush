using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreOfMass : MonoBehaviour
{

    public Vector3 centreOfMass1;
    public Rigidbody rb;
    public bool Awake;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.centerOfMass = centreOfMass1;
        rb.WakeUp();
        Awake = !rb.IsSleeping();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + transform.rotation * centreOfMass1, 1f);
    }
}
