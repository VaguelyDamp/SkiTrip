using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    public bool isRagdoll;
    public bool startingState = false;

    public void SetRagdoll(bool enable)
    {
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
    }
}
