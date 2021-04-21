using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveEnemy : MonoBehaviour
{
    public bool isStunned;
    public float Timer=5;
    private float StunnedTime;
    private NavMeshAgent agent;
    private float OriginalSpeed;

    private void Start()
    {
        StunnedTime = Timer;
        agent = GetComponent<NavMeshAgent>();
        OriginalSpeed = agent.speed;
    }

    private void Update()
    {


        if (isStunned)
        {
            StunnedTime -= Time.deltaTime;
            if (StunnedTime<=0)
            {
                StopStunning();
            }
        }

    }

    private void StopStunning()
    {
        isStunned = false;
        agent.speed = OriginalSpeed;
        StunnedTime = Timer;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Bullet")
        {
            Stunn();
        }
    }

    private void Stunn()
    {
        isStunned = true;
        agent.speed = OriginalSpeed/2;
        StunnedTime = Timer;
    }
}
