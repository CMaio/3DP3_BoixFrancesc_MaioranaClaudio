using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPlayerController : MonoBehaviour, IRestartGame
{
    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera cam;

    [Header("JUMP")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float speedJump;

    [Header("MOVEMENT")]
    [SerializeField] private KeyCode forwardKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode runKey;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private GameManager gm;


    private float verticalSpeed = -1.0f, movementSpeed;
    private bool onGround, falling;
    private bool resetPos;


    [SerializeField] Checkpoint_classe currentCheckpoint;
    public void setCheckpoint(Checkpoint_classe checkpoint)
    {
        if(currentCheckpoint == null || currentCheckpoint.getIndex() < checkpoint.getIndex())
        {
            currentCheckpoint = checkpoint;

        }
    }

    private void Start()
    {
        gm.addRestartListener(this);
    }


    private void OnDestroy()
    {
       gm.removeRestartListener(this);
    }


    void Update()
    {
        Vector3 movement = Vector3.zero;

        Vector3 l_forward = cam.transform.forward;
        l_forward.y = 0.0f;
        l_forward.Normalize();
        
        Vector3 l_right = cam.transform.right;
        l_right.y = 0.0f;
        l_right.Normalize();

        if (Input.GetKey(forwardKey)) movement += l_forward;

        if (Input.GetKey(backKey)) movement -= l_forward;

        if (Input.GetKey(rightKey)) movement += l_right;

        if (Input.GetKey(leftKey)) movement -= l_right;

        float currentSpeed = Input.GetKey(runKey) ? runSpeed : walkSpeed;

        if (movement.magnitude > 0)
        {
            movementSpeed = (movement.z * currentSpeed)/ movement.z;
            Debug.Log("MOVE: " + movementSpeed);

            movement.Normalize();
            transform.forward = movement.normalized;
            movement *= currentSpeed * Time.deltaTime;
        }
        else movementSpeed = 0.0f;

        if (Input.GetKeyDown(jumpKey) && onGround) jump();

        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        movement.y += verticalSpeed * Time.deltaTime;

        CollisionFlags cf = controller.Move(movement);
        if((cf & CollisionFlags.Below) != 0)
        {
            
            onGround = true;
            falling = false;
            verticalSpeed = -1.0f;
        }
        else
        {
            if ((cf & CollisionFlags.Above) != 0 && verticalSpeed > 0) verticalSpeed = 0.0f;
            onGround = false;
        }

        if (verticalSpeed < 0.0f) falling = true;



        animator.SetBool("onGround", onGround);
        animator.SetBool("falling", falling);
        animator.SetFloat("Speed", movementSpeed);

    }
    private void jump()
    {
        verticalSpeed = speedJump;
        animator.SetTrigger("jump");
    }

    void IRestartGame.RestartGame()
    {
        Debug.Log("dierestart");
        /*audioManager.Play("theme");*/
        GetComponent<CharacterController>().enabled = false;
        transform.position = currentCheckpoint.getCheckpointTransform().position;
        transform.rotation = currentCheckpoint.getCheckpointTransform().rotation;
        GetComponent<CharacterController>().enabled = true;
    }

    public void Die()
    {
        GetComponent<CharacterController>().enabled = false;
    }
}
