using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private float currentSpeed;
    private float originalSpeed;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        originalSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = agent.velocity.magnitude / originalSpeed;
        animator.SetFloat("Speed", currentSpeed, 0.1f, Time.deltaTime);
    }
}
