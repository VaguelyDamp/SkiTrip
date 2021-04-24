using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiSFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string turnEvent;
    [FMODUnity.EventRef]
    public string idleEvent;

    public float turnDuration = 1f;

    private FMOD.Studio.EventInstance idleInstance;
    private FMOD.Studio.EventInstance turnInstance;

    private PlayerController playerController; 

    void Start ()
    {
        playerController = 
            gameObject.GetComponent<PlayerController>();
        idleInstance = FMODUnity.RuntimeManager
            .CreateInstance(idleEvent);
        idleInstance.start();
        playerController.Steer += (value) => SteerSfx(value);
    }

    public void SteerSfx (float steerInputValue)
    {
        if (Mathf.Abs(steerInputValue) > 0.3f)
        {
            turnInstance = FMODUnity.RuntimeManager
                .CreateInstance(turnEvent);
            turnInstance.setParameterByName(
                "SteeringInput",
                steerInputValue
            );
            turnInstance.start();
        }
    }

    private void OnDeath ()
    {
        idleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        turnInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
