using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyProjectile : MonoBehaviour
{
    private bool collided = false;
    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet" && !collided)
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
