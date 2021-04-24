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

    public bool move = true;

    private Vector3 moveVec = new Vector3(0,0,0);

    public delegate void SteerEvent(float steerInputValue);
    public delegate void LoadCheckpointEvent(int checkpoint);

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
        Steer?.Invoke(value.Get<float>());
        horizantalInput = value.Get<float>();
    }

    //These are all dev hacks which is why bad code
    void OnCheckpoint1 (InputValue value)
    {
        OnLoadCheckpoint(1);
    }
    void OnCheckpoint2 (InputValue value)
    {
        OnLoadCheckpoint(2);
    }
    void OnCheckpoint3 (InputValue value)
    {
        OnLoadCheckpoint(3);
    }
    void OnCheckpoint4 (InputValue value)
    {
        OnLoadCheckpoint(4);
    }
    void OnCheckpoint5 (InputValue value)
    {
        OnLoadCheckpoint(5);
    }
    void OnCheckpoint6 (InputValue value)
    {
        OnLoadCheckpoint(6);
    }
    void OnCheckpoint7 (InputValue value)
    {
        OnLoadCheckpoint(7);
    }
    //end of bad dev hacks for now

    public event LoadCheckpointEvent LoadCheckpoint;
    void OnLoadCheckpoint (int checkpoint)
    {
        LoadCheckpoint?.Invoke(checkpoint);
    }

    private void Update()
    {
        if (move) 
        {
            Move();
        }
        else
        {

        };
    }

    private void Move()
    {
        horiMove = Mathf.MoveTowards(horiMove, horizantalInput, maxAccel);
        moveVec = new Vector3(horiMove*horizantalSpeed, 0, forwardSpeed);
        moveVec += Physics.gravity*gravityModifier;
        characterController.Move(Time.deltaTime * moveVec);
    }
}
