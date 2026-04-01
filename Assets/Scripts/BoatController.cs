using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float thrustForce = 10f;
    public float turnTorque = 10f;
    public float maxSpeed = 5f;
    public float weight = 0f;
    public float weightScale = 10f;

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

        float weightMultiplier = 1f / (1f + weight / weightScale);
        float effectiveThrust = thrustForce * weightMultiplier;
        float effectiveMaxSpeed = maxSpeed * weightMultiplier;

        rigidbody.AddForce(transform.forward * v * effectiveThrust);
        rigidbody.AddTorque(Vector3.up * h * turnTorque);

        Vector3 horizontalVel = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        if(horizontalVel.magnitude > effectiveMaxSpeed)
        {
            horizontalVel = horizontalVel.normalized * effectiveMaxSpeed;
            rigidbody.velocity = new Vector3(horizontalVel.x, rigidbody.velocity.y, horizontalVel.z);
        }
    }
}
