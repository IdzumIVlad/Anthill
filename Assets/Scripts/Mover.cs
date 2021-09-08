using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float gravity = -1f;
    private Rigidbody rb;
    private CharacterController characterController;
    private Vector3 moveVector;
    private Animator animator;
    private Joystick joystick;
    private Baggage baggage;

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); //прокинуть руками
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<Joystick>();
        baggage = GetComponent<Baggage>();
    }

    private void Update()
    {
        CharacterMove();
        GamingGravity();

        if (Input.GetKeyDown("space") && baggage.currentResAmount > 0)
        {
            foreach(Baggage.ResSlot curRes in baggage.resSlots)
            {
                if (curRes.resAmount > 0) 
                    baggage.DecreaseRes(curRes.resTypes); // отдать ресурс в мувере, так как нажатие пробела относится к управлению (правильно ли?)
            }
        };
    }

    private void CharacterMove() // use velocity
    {
        moveVector = Vector3.zero; 
        moveVector.x = joystick.Horizontal() * speed;
        moveVector.z = joystick.Vertical() * speed;

        animator.SetBool("Move", moveVector.x != 0 || moveVector.z != 0);

        if(moveVector != Vector3.zero) transform.forward = Vector3.Lerp(transform.forward, moveVector, 0.1f); 
        /*
        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0f)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, speed, 0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }*/

        moveVector.y = gravity;

        characterController.Move(moveVector * Time.deltaTime);
    }

    private void GamingGravity()
    {
        if (!characterController.isGrounded) gravity -= 20f;
        else gravity = -1f;
    }


}
