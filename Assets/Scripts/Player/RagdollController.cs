using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    public bool startingState = false;

    public bool Ragdoll
    {
        get { return Ragdoll; }
        set
        {
            Ragdoll = value;
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = !value;
                rb.detectCollisions = value;
            }
        }
    }

    private void Start()
    {
        Ragdoll = startingState;
    }
}
