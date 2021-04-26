using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool ccIsGrounded;
    private bool isCrouched;

    public CharacterController characterController;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public GameObject model;

    public Animator animator;

    private PlayerCollision playerCollision;
    private CapsuleCollider pbCol;

    public float horizantalSpeed = 5;
    public float[] speeds;
    public Quaternion[] slopRotations;
    private float forwardSpeed;
    public int speedIndex;
    public float maxAccel = .5f;
    public float gravityModifier = 0.2f;
    public float gravity = 10f;

    public float maxRotationAngle = 20f;
    public float rotateAccel = .1f;

    private float horizantalInput;
    private float horiMove = 0;
    private float horiAnim = 0;
    public float vSpeed = -10;
    private float yRot = 0;
    private float xRot = 0;

    public bool move = true;

    public GameObject pauseMenu;
    public bool paused = false;

    public float applyRamp = 0;
    public float rampSpeedInc = 1;
    public float maxRampSpeed = 8;
    public float gravInc = 0.1f;
    public float maxGrav = -10;

    private float airXRot = 0;
    private float airYRot = 0;
    private float airZRot = 0;

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

        playerCollision = gameObject.GetComponent<PlayerCollision>();

        currentRotation = slopRotations[speedIndex];

        speedIndex = 0;
        forwardSpeed = speeds[speedIndex];

        isCrouched = false;

        pbCol = transform.Find("BodyDeathCollider").GetComponent<CapsuleCollider>();
    }

    private void OnDeath()
    {
        characterController.enabled = false;
        virtualCamera.enabled = false;

        transform.Find("SkiPerson_Anim").gameObject.SetActive(false);
        transform.Find("SkiPerson_Ragdoll").gameObject.SetActive(true);
        gameObject.GetComponent<RagdollController>().SetRagdoll(true);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
        if (characterController.isGrounded) horizantalInput = value.Get<float>();
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
    void OnCheckpoint8 (InputValue value)
    {
        OnLoadCheckpoint(8);
    }
    void OnCheckpoint9 (InputValue value)
    {
        OnLoadCheckpoint(9);
    }
    void OnCheckpoint10 (InputValue value)
    {
        OnLoadCheckpoint(10);
    }
    void OnCheckpoint11 (InputValue value)
    {
        OnLoadCheckpoint(11);
    }
    void OnCheckpoint12 (InputValue value)
    {
        OnLoadCheckpoint(12);
    }
    void OnCheckpoint13 (InputValue value)
    {
        OnLoadCheckpoint(13);
    }
        void OnCheckpoint14 (InputValue value)
    {
        OnLoadCheckpoint(14);
    }
        void OnCheckpoint15 (InputValue value)
    {
        OnLoadCheckpoint(15);
    }
        void OnCheckpoint16 (InputValue value)
    {
        OnLoadCheckpoint(16);
    }
        void OnCheckpoint17 (InputValue value)
    {
        OnLoadCheckpoint(17);
    }
        void OnCheckpoint18 (InputValue value)
    {
        OnLoadCheckpoint(18);
    }
        void OnCheckpoint19 (InputValue value)
    {
        OnLoadCheckpoint(19);
    }
        void OnCheckpoint20 (InputValue value)
    {
        OnLoadCheckpoint(20);
    }
        void OnCheckpoint21 (InputValue value)
    {
        OnLoadCheckpoint(21);
    }
    //end of bad dev hacks for now

    void OnCrouch (InputValue value)
    {
        isCrouched = !isCrouched;
        Debug.Log("Crouched: " + isCrouched);
        if (animator)
        {
            animator.SetBool("Crouched", isCrouched);
        }

        if (isCrouched)
        {
            pbCol.height = 0.75f;
            characterController.height = 0.75f;
            pbCol.center = new Vector3(0, 0.45f, -0.05f);
            characterController.center = new Vector3(0, 0.45f, -0.05f);
        }
        else
        {
            pbCol.height = 1.5f;
            characterController.height = 1.5f;
            pbCol.center = new Vector3(0, 0.8f, -0.05f);
            characterController.center = new Vector3(0, .8f, -0.05f);
        }
    }

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
        animator.GetCurrentAnimatorStateInfo(0);
    }

    private void FixedUpdate()
    {
        if (!characterController.isGrounded)
        {
            airXRot = Mathf.MoveTowardsAngle(transform.eulerAngles.x, 10, 2);
            airYRot = Mathf.MoveTowardsAngle(transform.eulerAngles.y, 0, 2);
            airZRot = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 0, 2);

            transform.eulerAngles = new Vector3(airXRot, airYRot, airZRot);
            //Debug.Log(transform.rotation.eulerAngles.x);
            if (transform.rotation.eulerAngles.x > 10)
            {
                transform.Rotate(transform.right, -1 * Time.deltaTime);
            }
            else
            {
                transform.Rotate(transform.right, 1 * Time.deltaTime);
            }
        }

        Move();
    }

    public void IncreaseSpeed()
    {
        speedIndex++;
        forwardSpeed = speeds[speedIndex];
        Debug.Log("Rotation: " + transform.rotation);
        currentRotation = slopRotations[speedIndex];
    }

    public void DecreaseSpeed()
    {
        speedIndex--;
        forwardSpeed = speeds[speedIndex];
        currentRotation = slopRotations[speedIndex];
    }

    public void MoveToCheckPoint(Vector3 position)
    {
        characterController.enabled = false;

        checkpointMove = position;
        Debug.Log("Moving to checkpoint: " + checkpointMove);
        Transform animModel = transform.Find("SkiPerson_Anim");
        gameObject.GetComponent<RagdollController>().SetRagdoll(false);
        transform.Find("SkiPerson_Ragdoll").gameObject.SetActive(false);
        

        movingToCheckPoint = true;
    }

    private void Move()
    {
        if (movingToCheckPoint)
        {
            Transform animModel = transform.Find("SkiPerson_Anim");
            animModel.gameObject.SetActive(true);
            animModel.transform.localEulerAngles = Vector3.zero;
            animModel.transform.localPosition = Vector3.zero;

            transform.position = checkpointMove;
            transform.rotation = currentRotation;
            movingToCheckPoint = false;

            virtualCamera.enabled = true;
            characterController.enabled = true;
        }
        else
        {
            yRot = Mathf.MoveTowardsAngle(yRot, horizantalInput * maxRotationAngle, rotateAccel);
            model.transform.eulerAngles = new Vector3(model.transform.eulerAngles.x, yRot, transform.eulerAngles.z);

            ccIsGrounded = characterController.isGrounded;

            if (characterController.isGrounded)
            {
                horiMove = Mathf.MoveTowards(horiMove, horizantalInput, maxAccel);
                horiAnim = Mathf.MoveTowards(horiAnim, horizantalInput, .1f);
                if (animator)
                {
                    animator.SetFloat("Horizantal", horiAnim);
                    animator.SetBool("AirTime", false);
                }
            }
            else
            {
                Debug.Log("In Air wooooooosdofosdofsdosdfosdfofsdofdsosdfosfd");
                if (animator)
                {
                    animator.SetBool("AirTime", true);
                }
                horiMove = 0;
            }
            

            moveVec = new Vector3(horiMove * horizantalSpeed, 0, forwardSpeed);
            //moveVec += Physics.gravity * gravityModifier;
            if (applyRamp > 0)
            {
                vSpeed = Mathf.MoveTowards(vSpeed, maxRampSpeed, rampSpeedInc);
                applyRamp -= Time.deltaTime;
            }
            else
            {
                vSpeed = Mathf.MoveTowards(vSpeed, maxGrav, gravInc);
            }
            moveVec += new Vector3(0, vSpeed, 0);

            characterController.Move(Time.deltaTime * moveVec);
        }
    }
}
