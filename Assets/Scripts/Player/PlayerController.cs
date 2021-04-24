using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5;

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
        Move(value);
    }

    private void Move(InputValue value)
    {
       Debug.Log(value.Get<float>());
    }
}
