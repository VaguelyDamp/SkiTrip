using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Colliding with: " + col.gameObject);
        if (col.transform.tag == "Tree")
        {
            gameController.Death();
        }
    }
}
