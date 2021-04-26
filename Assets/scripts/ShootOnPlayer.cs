using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootOnPlayer : MonoBehaviour
{
    public Text projPower;
    public Camera cam;
    public GameObject projectile;
    public float speed = 30;
    public float power=1;
    public float fireRate = 3.0f;
    private float rate = 0;
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        rate += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && rate>fireRate)
        {
            rate = 0;
            shootProjectile();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            power++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            power--;
        }
        projPower.text = "power: " + power + "x";
    }

    void shootProjectile()
    {
        Vector3 destination;
        var projectileObject = Instantiate(projectile, firePoint.transform.position + firePoint.transform.forward.normalized * 0.1f, Quaternion.identity) as GameObject;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        projectileObject.GetComponent<Rigidbody>().mass *= power;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        projectileObject.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * speed;
        
    }
}
