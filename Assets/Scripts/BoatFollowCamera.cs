using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatFollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public Transform goalPos;
    public float smoothSpeed = 1f;

    void LateUpdate()
    {
        Vector3 delta = target.position - transform.position;
        float distMultiplier = delta.x * delta.x + delta.y * delta.y + delta.z * delta.z;

        transform.position = Vector3.MoveTowards(transform.position, goalPos.position, smoothSpeed);
        transform.LookAt(target);
    }
}
