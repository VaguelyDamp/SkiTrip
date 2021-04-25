using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiSFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string turnEvent;
    [FMODUnity.EventRef]
    public string idleEvent;
    [FMODUnity.EventRef]
    public string deathEvent;

    public float turnDuration = 1f;

    private bool dead = false;

    private GameController gameController;

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
        playerController.PauseToggle += (paused) => OnPauseToggle(paused);
        gameController = GameObject.Find("GameController")
            .GetComponent<GameController>();
        gameController.OnDeath += OnDeath;
    }

    private void OnPauseToggle (bool paused)
    {
        idleInstance.setPaused(paused);
    }

    public void SteerSfx (float steerInputValue)
    {
        if (dead) return;
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
        FMODUnity.RuntimeManager
            .PlayOneShot(deathEvent);
        idleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        turnInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
