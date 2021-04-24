using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float horizantalSpeed = 5;
    public float forwardSpeed = 4;
    public float maxAccel = .5f;
    public float gravityModifier = 0.2f;

    private float horizantalInput;
    private float horiMove = 0;

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
        Steer?.Invoke();
        horizantalInput = value.Get<float>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        horiMove = Mathf.MoveTowards(horiMove, horizantalInput, maxAccel);
        moveVec = new Vector3(horiMove*horizantalSpeed, 0, forwardSpeed);
        moveVec += Physics.gravity*gravityModifier;
        characterController.Move(Time.deltaTime * moveVec);
    }
}
