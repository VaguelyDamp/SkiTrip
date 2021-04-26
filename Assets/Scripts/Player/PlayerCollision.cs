using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;

    public bool ramping = false;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Colliding with: " + col.gameObject.name);
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

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Colliding trigger with: " + col.transform.parent);
        if (col.transform.tag == "Death")// || 
            //col.transform.parent?.transform.tag == "Rock" ||
            //col.transform.parent?.transform.parent.tag == "Prop")
        {
            gameController.Death();
        }

        if (col.gameObject.name == "EpicBigChungusFloor")
        {
            Debug.Log("Hello");
            playerController.OnWin();
        }

        if (col.transform.tag == "Collectable")
        {
            Collectable collect = col.transform.parent.gameObject.GetComponent<Collectable>();
            Collectable.CollectableType ctype = collect.collectableType;
            Debug.Log("Collectable found: " + ctype);
            PlayerPrefs.SetFloat(collect.collectableType.ToString()+collect.phase, 1);
        }
    }
}
