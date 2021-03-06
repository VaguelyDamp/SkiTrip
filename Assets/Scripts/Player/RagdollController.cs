using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    //public Rigidbody[] rigidbodies;
    public bool isRagdoll;
    public bool startingState = false;

    private GameController gameController;

    public void SetRagdoll(bool enable)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach(GameObject rbo in GameObject.FindGameObjectsWithTag("RagdollRB"))
        {
            rigidbodies.Add(rbo.GetComponent<Rigidbody>());
        }

        isRagdoll = enable;
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !enable;
            rb.detectCollisions = enable;
        }
    }

    private void Start()
    {
        SetRagdoll(startingState);

        gameController = GameObject.Find("GameController")
            .GetComponent<GameController>();
        gameController.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        //SetRagdoll(true);
    }
}
