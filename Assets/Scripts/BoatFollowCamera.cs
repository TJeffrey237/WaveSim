using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatFollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public Transform goalPos;
    public float smoothTime = 0.25f;

    private Vector3 velocity;

    void LateUpdate()
    {
        Vector3 targetPosition = goalPos ? goalPos.position : target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        if (target)
        {
            transform.LookAt(target);
        }
    }
}
