using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForce : MonoBehaviour
{
    [SerializeField] private float playerWeight;
    private float defaultSpeed = 8;
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        MainCameraFollow cam = GameObject.Find("Main Camera").GetComponent<MainCameraFollow>();
        Vector3 impulse = collision.impulse;
        Vector3 collisionForce = impulse / Time.fixedDeltaTime;
        //0.1 left, -0.1 right in the point  y=1 center
        Vector3 point= transform.InverseTransformPoint(contact.point);
        //interia on the player rigid body
        float force = impulse.magnitude;
        float angle = CalcAngle(force);
        float speedFactor = CalcSpeed(force);
        
        if (speedFactor<=1)
        {
            cam.shakeCamera(0.1f, 0.05f);
        }else
        if (speedFactor>=3) {
            cam.shakeCamera(speedFactor/2, speedFactor/10);
            cam.beDizzy(speedFactor/2,speedFactor*10);

        }
        if (point.x<0)
        {
            cam.RotateCam(angle, speedFactor * defaultSpeed, 0);
            //right
        }
        else if (point.x>0)
        {
            //left 
            cam.RotateCam(-1*angle, speedFactor * defaultSpeed, 0);

        }
        else if (point.x==0)
        {
            //back
            cam.moveCameraBack(speedFactor, speedFactor);
        }
        
    }
    public float CalcAngle(float force)
    {
        if (force <= playerWeight)
        {
            return force / playerWeight * 90;
        }
        else
        {
            return 90;
        }
    }

    public float CalcSpeed(float force)
    {
        if (force<=playerWeight)
        {
            return 1;
        }
        else
        {
            return force / playerWeight;
        }
    }
}
