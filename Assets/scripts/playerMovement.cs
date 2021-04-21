using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float mouseSensetivity;


    private Vector3 moveDirection;
    private Vector3 velocity;
    private CharacterController controller;
    private Animator animator;
    private bool died;
    private AudioManager am;
    private enemiesController enemiesControllerScript;

    private void Start()
    {
        enemiesControllerScript = FindObjectOfType<enemiesController>();
        am = FindObjectOfType<AudioManager>();

        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        playerJson t = JsonUtility.FromJson<playerJson>(new JSONDemo("demo.json").getJson());
        transform.position = t.position;
        Quaternion thetas = Quaternion.Euler(t.rotation.x, t.rotation.y, t.rotation.z);

        transform.rotation = thetas;
        transform.localScale = t.scale;
       

    }
    private void Update()
    {
        if (!died)
            Move();
        if (Score.boxScore>=10)
        {
            am.Play("win", true);
            pauseMenuScript.youWon();
        }

    }
    
    private void Move()
    {
        Rotate();
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded&&velocity.y<0)
        {
            velocity.y = -2;
        }
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();

            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)

            {
                Idle();
            }
            moveDirection *= moveSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

        }else
        {
            animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
        }
        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    private void Idle()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1.0f,0.1f,Time.deltaTime);

    }
    private void Jump()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);

        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void resetPosition()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);

        transform.position = new Vector3(0, 10, 1);
    }
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX);
    }
    private void Fire()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (Score.currentHealth>0)
            {
                Score.currentHealth -= 50;
                if (Score.currentHealth<=0)
                {
                    Score.lifes -= 1;
                    if (Score.lifes<=0)
                    {
                        am.Play("death", true);

                        pauseMenuScript.gameOver();
                        return;
                    }
                    animator.SetBool("isDying", true);
                    animator.SetBool("isLiving", true);
                    animator.SetBool("isBackToLive", true);
                    am.Play("pain",true);
                    Invoke("live", 2.0f);
                    StartCoroutine(DisableScript());
                    
                    Score.currentHealth = 100;
                }
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    private void live()
    {
        animator.SetBool("isDying", false);
    }

    IEnumerator DisableScript()
    {
        enemiesControllerScript.enabled = false;

        died = true;
        yield return new WaitForSeconds(14f);
        animator.SetBool("isDying", false);
        animator.SetBool("isLiving", false);
        animator.SetBool("isBackToLive", false);
        died = false;

        enemiesControllerScript.enabled = true;
    }




}



[System.Serializable]
class playerJson
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

}

//in the start method
