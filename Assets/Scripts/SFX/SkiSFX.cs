using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiSFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string turnEvent;
    [FMODUnity.EventRef]
    public string longTurnEvent;
    [FMODUnity.EventRef]
    public string idleEvent;
    [FMODUnity.EventRef]
    public string deathEvent;

    public float turnDuration = 1f;

    private bool dead = false;

    private GameController gameController;

    private FMOD.Studio.EventInstance idleInstance;
    private FMOD.Studio.EventInstance turnInstance;
    private FMOD.Studio.EventInstance longTurnInstance;

    private PlayerController playerController; 

    private bool longTurning = false;

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

            if (longTurning) StartCoroutine(FadeOutEvent(longTurnInstance, 0.7f));
            else longTurning = true;
            longTurnInstance =  FMODUnity.RuntimeManager
                .CreateInstance(longTurnEvent); 
            longTurnInstance.setParameterByName(
                "SteeringInput",
                steerInputValue
            );
            longTurnInstance.start();
        }
        else if (longTurning)
        {
            longTurning = false;
            StartCoroutine(FadeOutEvent(longTurnInstance, 0.7f));
        }
    }


    private IEnumerator FadeOutEvent (FMOD.Studio.EventInstance instance, float fadeOutTime)
    {
        float timeElapsed = 0f;
        while (timeElapsed < fadeOutTime)
        {
           instance.setParameterByName(
                    "FadeOutTurn", 
                    Remap(
                        timeElapsed,
                        0f,
                        fadeOutTime,
                        0f,
                        1f
                    ));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDeath ()
    {
        FMODUnity.RuntimeManager
            .PlayOneShot(deathEvent);
        idleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        turnInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        longTurnInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private IEnumerator UnDie ()
    {
        
        yield return new WaitForSeconds(GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().deathTime);
        idleInstance.start();
    }

    public float Remap(float value, float fromStart, float fromEnd, float toStart, float toEnd)
    {
        float t = (value - fromStart) / (fromEnd - fromStart);
        return t * (toEnd - toStart) + toStart;
    }
}
