using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float thrustForce = 10f;
    public float turnTorque = 10f;
    public float maxSpeed = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        if(!rigidbody) rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        rigidbody.AddForce(transform.forward * v * thrustForce);
        rigidbody.AddTorque(Vector3.up * h * turnTorque);
    }
}
