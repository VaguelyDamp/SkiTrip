using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;

    public bool ramping = false;

    private bool thisIsOnGround; 
    public bool isOnGround
    {
        get
        {
            if (thisIsOnGround)
            {
                thisIsOnGround = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Colliding with: " + col.gameObject);
        if (col.transform.tag == "Tree" || 
            col.transform.parent?.transform.tag == "Rock" ||
            col.transform.parent?.transform.parent.tag == "Prop")
        {
            gameController.Death();
        }

        if(col.transform.tag == "Ramp")
        {
            //ramping = true;
            //Debug.Log("Start Ramp");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "ForceGate")
        {
            //ramping = false;
            Debug.Log("Applying ramp force (trigger)");
            playerController.applyRamp = other.transform.parent.GetComponent<Ramp>().accelTime;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.layer == 6 || col.transform.tag == "Ramp")
        {
            thisIsOnGround = true;
        }
    }
}
