using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForce : MonoBehaviour
{
    [SerializeField] private float playerWeight;
    private void OnCollisionEnter(Collision collision)
    {

        ContactPoint contact = collision.contacts[0];
        MainCameraFollow cam = GameObject.Find("Main Camera").GetComponent<MainCameraFollow>();
        Vector3 impulse = collision.impulse;
        Vector3 collisionForce = impulse / Time.fixedDeltaTime;
        //0.1 left, -0.1 right in the point  y=1 center
        Vector3 point= transform.InverseTransformPoint(contact.point);
        print(point);
        print(impulse.magnitude);
        print(collision.gameObject);
        if (point.x<0)
        {
            print("right");
            //right
        }
        if (point.x>0)
        {
            //left 
            print("left");

        }
        if (point.x==0)
        {
            //back
            print("center");
        }
        cam.RotateCam(90,4,0);
        


        
    }
}
