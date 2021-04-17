using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveProjectile : MonoBehaviour
{
    private bool collided=false;
    public GameObject explosionEffect;
    
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag!="Player"&& collision.gameObject.tag != "Bullet"&& !collided)
        {
            collided = true;
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);


        }
    }
    public void destroyBullet()
    {
        Destroy(gameObject, 2.0f);

    }
}
