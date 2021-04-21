﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemiesController : MonoBehaviour
{
    private NavMeshAgent enemy1;
    private NavMeshAgent enemy2;
    private NavMeshAgent enemy3;
    public GameObject Player;
    public GameObject en1;
    public GameObject en2;
    public GameObject en3;
    public GameObject projectile;
    public float projectileSpeed=2;
    private GameObject currentBullet;
    public float distanceToPlayer = 3;
    public float FireRate = 2.0f;
    private float fireRate;
    private AudioManager am;
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        enemy1 = en1.GetComponent<NavMeshAgent>();
        enemy2 = en2.GetComponent<NavMeshAgent>();
        enemy3 = en3.GetComponent<NavMeshAgent>();
        fireRate = FireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        float distance1 = Vector3.Distance(en1.transform.position, Player.transform.position);
        float distance2 = Vector3.Distance(en2.transform.position, Player.transform.position);
        float distance3 = Vector3.Distance(en3.transform.position, Player.transform.position);
        if (distance1 <= distance2 && distance1 <= distance3)
        {
            if (distance1 >= distanceToPlayer)
            {
                enemy1.SetDestination(Player.transform.position);
            }else
            {
                am.Play("run",true);
                enemy1.SetDestination(en1.transform.position);
                if (Player.transform.position.y <= 1.5 && distance1 > distanceToPlayer / 10)
                {
                    LookAtPlayer(Player.transform, en1.transform);
                }
                shootPlayer(en1);
                
            }

            if (coinsController.currentBox!=null)
            {
                distance2= Vector3.Distance(en2.transform.position, coinsController.currentBox.transform.position);
                distance3= Vector3.Distance(en3.transform.position, coinsController.currentBox.transform.position);
                if (distance2<=distance3)
                {
                    enemy2.SetDestination(coinsController.currentBox.transform.position);
                    Wander(enemy3);
                }else
                {
                    enemy3.SetDestination(coinsController.currentBox.transform.position);
                    Wander(enemy2);
                }
            }

        }
        else if (distance2<=distance1&&distance2<=distance3)
        {
            if (distance2 >= distanceToPlayer)
            {
                enemy2.SetDestination(Player.transform.position);
            }else
            {
                am.Play("run", true);
                enemy2.SetDestination(en2.transform.position);
                if (Player.transform.position.y <= 1.5 && distance2 > distanceToPlayer / 10)
                {
                    LookAtPlayer(Player.transform, en2.transform);
                }
                shootPlayer(en2);
            }
            if (coinsController.currentBox != null)
            {
                distance1 = Vector3.Distance(en1.transform.position, coinsController.currentBox.transform.position);
                distance3 = Vector3.Distance(en3.transform.position, coinsController.currentBox.transform.position);
                if (distance1 <= distance3)
                {
                    enemy1.SetDestination(coinsController.currentBox.transform.position);
                    Wander(enemy3);
                }
                else
                {
                    enemy3.SetDestination(coinsController.currentBox.transform.position);
                    Wander(enemy1);
                }
            }
        }
        else if (distance3<=distance1&& distance3<=distance2)
        {
            if (distance3 >= distanceToPlayer)
            {
                enemy3.SetDestination(Player.transform.position);
            }else
            {
                am.Play("run", true);
                enemy3.SetDestination(en3.transform.position);
                LookAtPlayer(Player.transform, en3.transform);
                shootPlayer(en3);
            }
            if (coinsController.currentBox != null)
            {
                distance1 = Vector3.Distance(en1.transform.position, coinsController.currentBox.transform.position);
                distance2 = Vector3.Distance(en2.transform.position, coinsController.currentBox.transform.position);
                if (distance1 <= distance2)
                {
                    enemy1.SetDestination(coinsController.currentBox.transform.position);
                    Wander(enemy2);
                }
                else
                {
                    enemy2.SetDestination(coinsController.currentBox.transform.position);
                    Wander(enemy1);
                }
            }
        }


    }
    public void Wander(NavMeshAgent agent)
    {
        Vector3 newPos = RandomNavSphere(new Vector3(0,0,0), 12, -1);
        agent.SetDestination(newPos);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void shootPlayer(GameObject firePoint)
    {
        if (fireRate < 0 && currentBullet == null)
        {
            var projectileObject = Instantiate(projectile, firePoint.transform.position + firePoint.transform.forward.normalized * 0.3f, Quaternion.identity) as GameObject;
            currentBullet = projectileObject;
            currentBullet.GetComponent<Rigidbody>().velocity = (Player.transform.position - firePoint.transform.position).normalized * projectileSpeed;
            MoveEnemyProjectile sc = currentBullet.GetComponent<MoveEnemyProjectile>();
            sc.destroyBullet();
            fireRate = FireRate;
        }
    }

    public void LookAtPlayer(Transform target,Transform shooter)
    {
        var lookPos = target.position - shooter.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        shooter.rotation = Quaternion.Slerp(shooter.rotation, rotation, Time.deltaTime * 2);
    }
}
