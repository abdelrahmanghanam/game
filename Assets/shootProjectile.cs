using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class shootProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public GameObject projectile;
    private Vector3 destination;
    public Transform firePoint;
    public float speed=30;
    public GameObject currentBullet;
    private bool moved;
    private Animator animator;



    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenuScript.gameIsPaused)
        {
            if (Input.GetButtonDown("Fire1") && currentBullet != null && !moved)
            {
                animator.SetBool("IsFiring", true);
                moveIt();
                moved = true;
            }
            else
            {
                animator.SetBool("IsFiring", false);
            }
            if (Input.GetButtonDown("Fire1") && currentBullet == null && Score.bottlesScore > 0)
            {
                shootProj();
                moved = false;
                Score.bottlesScore--;
            }

            //try follow
            if (currentBullet != null && !moved)
            {
                currentBullet.transform.position = firePoint.transform.position+firePoint.transform.forward.normalized*0.1f;
                currentBullet.transform.rotation = firePoint.transform.rotation;
            }

        }

        
    }
    private void shootProj()
    {

        InsetintiateProj(firePoint);
        
    }
    private void InsetintiateProj(Transform fp)
    {
        var projectileObject = Instantiate(projectile, firePoint.transform.position + firePoint.transform.forward.normalized * 0.1f, Quaternion.identity) as GameObject;
        currentBullet = projectileObject;
//        moveIt();
    }
    private void moveIt()
    {
        ParticleSystem ps =  currentBullet.GetComponentInChildren<ParticleSystem>();



        ParticleSystem.ColorOverLifetimeModule settings = ps.colorOverLifetime;
        settings.color = new ParticleSystem.MinMaxGradient(new Color(0,1,0),new Color(0,0,1));


        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        if (currentBullet != null)
        {
            currentBullet.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * speed;
        }
        moveProjectile sc= currentBullet.GetComponent<moveProjectile>();
        sc.destroyBullet();
    }
}
