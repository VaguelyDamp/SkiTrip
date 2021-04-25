using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public GameObject model;

    public float horizantalSpeed = 5;
    public float forwardSpeed = 4;
    public float maxAccel = .5f;
    public float gravityModifier = 0.2f;

    public float maxRotationAngle = 20f;
    public float rotateAccel = .1f;

    private float horizantalInput;
    private float horiMove = 0;
    private float yRot = 0;

    public bool move = true;

    public GameObject pauseMenu;
    public bool paused = false;

    private Vector3 moveVec = new Vector3(0,0,0);

    private GameController gameController;

    public delegate void SteerEvent(float steerInputValue);
    public delegate void LoadCheckpointEvent(int checkpoint);
    public delegate void PauseEvent(bool paused);

    private bool movingToCheckPoint = false;
    private Vector3 checkpointMove;

    private Quaternion currentRotation;
 

    void Start ()
    {
        gameController = GameObject.Find("GameController")
            .GetComponent<GameController>();
        gameController.OnDeath += OnDeath;

        currentRotation = new Quaternion(0.1f, 0.0f, 0.0f, 1.0f);
    }

    private void OnDeath()
    {
        characterController.enabled = false;
        virtualCamera.enabled = false;
    }

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
    public void OnLoadCheckpoint (int checkpoint)
    {
        Resume();
        LoadCheckpoint?.Invoke(checkpoint);
    }

    public event PauseEvent PauseToggle;
    void OnPause (InputValue value)
    {
        paused = !paused;
        PauseToggle?.Invoke(paused);
        pauseMenu.SetActive(paused);
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Resume ()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseToggle?.Invoke(false);
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

    public void MoveToCheckPoint(Vector2 position)
    {
        checkpointMove = new Vector3(0, position.x, position.y);
        Debug.Log("Moving to checkpoint: " + checkpointMove);
        movingToCheckPoint = true;
    }

    private void Move()
    {
        if (movingToCheckPoint)
        {
            characterController.enabled = false;
            transform.position = checkpointMove;
            transform.rotation = currentRotation;
            movingToCheckPoint = false;
            virtualCamera.enabled = true;
            //Debug.Log("Rotation: " + transform.rotation);
            characterController.enabled = true;
            //Debug.Log("Actually applied checkpoint move");
        }
        else
        {
            yRot = Mathf.MoveTowardsAngle(yRot, horizantalInput * maxRotationAngle, rotateAccel);
            model.transform.eulerAngles = new Vector3(model.transform.eulerAngles.x, yRot, transform.eulerAngles.z);

            //transform.Rotate(transform.up, horizantalInput * maxRotationAngle * rotateAccel * Time.deltaTime);

            horiMove = Mathf.MoveTowards(horiMove, horizantalInput, maxAccel);
            moveVec = new Vector3(horiMove * horizantalSpeed, 0, forwardSpeed);
            moveVec += Physics.gravity * gravityModifier;
            characterController.Move(Time.deltaTime * moveVec);
        }
    }
}
