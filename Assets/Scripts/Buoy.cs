using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float buoyancy = 1f;
    public float damping = 2f;
    public float heightSmoothing = 3f;
    public float maxBuoyForce = 1000f;

    private List<Wave> waves;
    private float smoothedWaterHeight;

    // Start is called before the first frame update
    void Start()
    {
        waves = FindObjectsOfType<Wave>().ToList();
        smoothedWaterHeight = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float waterHeight = 0f;
        foreach(var wave in waves)
        {
            waterHeight += wave.GetHeight(transform.position.x, transform.position.z);
        }

        float smoothFactor = 1f - Mathf.Exp(-heightSmoothing * Time.fixedDeltaTime);
        smoothedWaterHeight = Mathf.Lerp(smoothedWaterHeight, waterHeight, smoothFactor);

        float submersion = smoothedWaterHeight - transform.position.y;
        if (submersion > 0f)
        {
            float buoyancyForce = rigidbody.mass * Physics.gravity.magnitude * buoyancy * submersion;
            float dampingForce = -rigidbody.velocity.y * damping * rigidbody.mass;
            float totalForce = Mathf.Clamp(buoyancyForce + dampingForce, -maxBuoyForce, maxBuoyForce);
            rigidbody.AddForceAtPosition(Vector3.up * totalForce, transform.position);
        }
    }
}
