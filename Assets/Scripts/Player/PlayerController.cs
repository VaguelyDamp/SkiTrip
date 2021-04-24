using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5;

    PlayerInputs inputActions;

    void Start()
    {
        inputActions = new PlayerInputs(); 
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Debug.Log(inputActions.Steering.Move.ReadValue<float>());
    }
}
