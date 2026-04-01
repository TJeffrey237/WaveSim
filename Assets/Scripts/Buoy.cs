using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float buoyancy = 1f;

    private List<Wave> waves;

    // Start is called before the first frame update
    void Start()
    {
        waves = FindObjectsOfType<Wave>().ToList();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float waterHeight = 0f;
        foreach(var wave in waves)
        {
            waterHeight += wave.GetHeight(transform.position.x, transform.position.z);
        }

        if(transform.position.y < waterHeight)
        {
            float submersion = waterHeight - transform.position.y;
            float force = rigidbody.mass * Physics.gravity.magnitude * buoyancy * submersion;
            //rigidbody.AddForce(Vector3.up * force);
            rigidbody.AddForceAtPosition(Vector3.up * force, transform.position);
        }
    }
}
