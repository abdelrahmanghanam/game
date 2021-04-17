using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed=0.125f;
    public Vector3 offset;
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
        transform.rotation = target.transform.rotation;
        transform.LookAt(target.transform);
    }
}
