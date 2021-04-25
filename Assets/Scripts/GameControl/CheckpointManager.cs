using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CheckpointManager : MonoBehaviour
{
    public float deathTime = 2f;
    public float fadeInTime = 2f;

    public FMOD.Studio.EventInstance songTimeline;
    [FMODUnity.EventRef]
    public string startMusic;

    public float progressThroughGame = 0f;
    public float endOfGame = 147065f;

    private GameObject player;
    private PlayerController playerController;
    private GameController gameController;
    private UImanager uiManager;

    IDictionary<int, CheckpointData> checkpoints = new Dictionary<int, CheckpointData>()
    {
        {1, new CheckpointData(9047, 0)},
        {2, new CheckpointData(32997, 0)},
        {3, new CheckpointData(56999, 0)},
        {4, new CheckpointData(69081, 0)},
        {5, new CheckpointData(81059, 0)},
        {6, new CheckpointData(105098, 0)},
        {7, new CheckpointData(147065, 0)}
    };

    public int currentCheckpoint = 1;
    public int phase = 1;
    public int phase2Start = 3;
    public int phase3Start = 6;

    public float Remap(float value, float fromStart, float fromEnd, float toStart, float toEnd)
    {
        float t = (value - fromStart) / (fromEnd - fromStart);
        return t * (toEnd - toStart) + toStart;
    }

    void Start ()
    {
        //should change i < to be less than total number of checkpoint
        //gates actually implemented in game
        for (int i = 0; i < 7; i++)
        {
            checkpoints[i + 1].position = new Vector2(
                GameObject.Find("Checkpoint" + (i + 1))
                    .transform.position.y + 8f,
                GameObject.Find("Checkpoint" + (i + 1))
                    .transform.position.z - 16f
            );
        }
        songTimeline = FMODUnity.RuntimeManager.CreateInstance(startMusic);
        songTimeline.start();
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerController.LoadCheckpoint += (checkpoint) => LoadCheckpoint(checkpoint);
        playerController.PauseToggle += (paused) => OnPauseToggle(paused);
        gameController = GameObject.Find("GameController")
            .GetComponent<GameController>();
        gameController.OnDeath += OnDeath;
        uiManager = GameObject.Find("UI").GetComponent<UImanager>();
    }

    private void OnPauseToggle (bool paused)
    {
        songTimeline.setPaused(paused);
    }

    public void LoadCheckpoint (int checkpoint)
    {
        currentCheckpoint = checkpoint;
        playerController.move = false;
        player.transform.position = new Vector3(0f, 
            checkpoints[currentCheckpoint].position.x, 
            checkpoints[currentCheckpoint].position.y);
        SongTransition();
        uiManager.ChangeCheckpointMarkers(phase);
        if (currentCheckpoint == checkpoints.Count)
        {
           // TriggerWin();
        }
    }

    // public event WinEvent OnWin;
    // void TriggerWin (InputValue value)
    // {
    //     OnWin?.Invoke();
    // }

    public void OnDeath ()
    {
        songTimeline.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(UnDie());
    }

    private IEnumerator UnDie ()
    {
        yield return new WaitForSeconds(deathTime);
        songTimeline.start();
        LoadCheckpoint(currentCheckpoint);
    }

    private void SongTransition ()
    {
        songTimeline.setTimelinePosition(
            checkpoints[currentCheckpoint]
            .timelinePosition - (int)(fadeInTime * 1000));
        StartCoroutine(FadeSong());
        StartCoroutine(EnableControls());
    }

    private IEnumerator EnableControls ()
    {
        yield return new WaitForSeconds(1f);
        playerController.move = true;
    }

    private IEnumerator FadeSong ()
    {
        float timeElapsed = 0f;
        while (timeElapsed < fadeInTime)
        {
            FMODUnity.RuntimeManager.StudioSystem
                .setParameterByName(
                    "FadeMusic", 
                    Remap(
                        timeElapsed,
                        0f,
                        fadeInTime,
                        0f,
                        1f
                    ));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
    }

    void Update ()
    {
        if (currentCheckpoint + 1 >= phase3Start)
        {
            phase = 3;
        }
        else if (currentCheckpoint + 1 >= phase2Start)
        {
            phase = 2;
        }
        else
        {
            phase = 1;
        }
        int curPosition;
        songTimeline.getTimelinePosition(out curPosition);
        if (curPosition == 8900) Debug.Log("Now");
        if (checkpoints.Count >= currentCheckpoint + 1 &&
            curPosition >= checkpoints[currentCheckpoint + 1].timelinePosition)
        {
            currentCheckpoint++;
            uiManager.ChangeCheckpointMarkers(phase);
            Debug.Log("Current Checkpoint: " + currentCheckpoint);
        }
        if (currentCheckpoint == 1)
        {
            uiManager.ChangeCheckpointMarkers(1);
            Debug.Log("Current Checkpoint: " + currentCheckpoint);
        }
        float trackerProgress = Remap(
            curPosition,
            0f,
            endOfGame,
            0f,
            1f
        );
        progressThroughGame = trackerProgress;
    }
}
