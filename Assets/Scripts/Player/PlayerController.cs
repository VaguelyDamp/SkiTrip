using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5;
    private float hi;

    private float horizantalInput;

    private Vector3 moveVec = new Vector3(0,0,0);

    public delegate void SteerEvent();

    //OnSteer is automatically a thing because we have 
    //an action called "steer" in the input map thing
    //everything changed recently idk how to make it like
    //it is in divine drifter but this should work better
    //becuase you only ever subscribe to inputs in the
    //player controller and then subscribe to subsequent
    //events that we call in other scripts to handle the input
    public event SteerEvent Steer;
    void OnSteer(InputValue value)
    {
        //hi = value.Get<float>();
        //Debug.Log(hi);
        Debug.Log("poop");
        Steer?.Invoke();
<<<<<<< HEAD
        //Move(value);
=======
        horizantalInput = value.Get<float>();
    }

    private void FixedUpdate()
    {
        Move();
>>>>>>> c42bc4afbccf207344fbc6cc71d8aa17b5ced55d
    }

    private void Move()
    {
<<<<<<< HEAD
       //Debug.Log(value.Get<float>());
=======
        moveVec = new Vector3(horizantalInput, 0, 1);
        //if (!characterController.isGrounded)
        //{
            moveVec += Physics.gravity*.2f;
        //}
        characterController.Move(speed * Time.deltaTime * moveVec);
    }

    private void SteerLeft(InputValue value)
    {

>>>>>>> c42bc4afbccf207344fbc6cc71d8aa17b5ced55d
    }

    void Update ()
    {
        //Debug.Log(hi);
    }

}
