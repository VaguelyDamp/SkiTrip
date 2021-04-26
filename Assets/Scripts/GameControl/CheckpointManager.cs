using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CheckpointManager : MonoBehaviour
{
    public float deathTime = 1f;
    public float fadeInTime = 2f;

    public FMOD.Studio.EventInstance songTimeline;
    [FMODUnity.EventRef]
    public string startMusic;

    public float progressThroughGame = 0f;
    private float endOfGame = 524000f;

    private GameObject player;
    private PlayerController playerController;
    private GameController gameController;
    private UImanager uiManager;

    IDictionary<int, CheckpointData> checkpoints = new Dictionary<int, CheckpointData>()
    {
        {1, new CheckpointData(9047)},
        {2, new CheckpointData(32997)},
        {3, new CheckpointData(56999)},
        {4, new CheckpointData(69081)},
        {5, new CheckpointData(81059)},
        {6, new CheckpointData(105098)},
        {7, new CheckpointData(147065)},
        {8, new CheckpointData(171957)},
        {9, new CheckpointData(204054)},
        {10, new CheckpointData(236003)},
        {11, new CheckpointData(252008)},
        {12, new CheckpointData(284036)},
        {13, new CheckpointData(316043)},
        {14, new CheckpointData(351012)},
        {15, new CheckpointData(375000)},
        {16, new CheckpointData(396000)},
        {17, new CheckpointData(407000)},
        {18, new CheckpointData(428000)},
        {19, new CheckpointData(460010)},
        {20, new CheckpointData(481341)},
        {21, new CheckpointData(502700)}
    };

    public int currentCheckpoint = 1;
    public int phase = 1;
    private int phase2Start = 8;
    private int phase3Start = 14;

    public float Remap(float value, float fromStart, float fromEnd, float toStart, float toEnd)
    {
        float t = (value - fromStart) / (fromEnd - fromStart);
        return t * (toEnd - toStart) + toStart;
    }

    void Start ()
    {
        for (int i = 0; i < 21; i++)
        {
            GameObject checkpointGO = GameObject.Find("Checkpoint" + (i + 1));
            checkpoints[i + 1].position = checkpointGO.transform.Find("RespawnPoint").position;
            //checkpoints[i + 1].position.y += 10f;
            //checkpoints[i + 1].position.z -= 30f;
            checkpoints[i + 1].vcam = checkpointGO.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
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
        foreach (CheckpointData cpd in checkpoints.Values)
        {
            cpd.vcam.enabled = false;
            Debug.Log(cpd.vcam);
        }
        checkpoints[currentCheckpoint].vcam.enabled = true;
        //playerController.move = false;
        if (checkpoint >= phase3Start)
        {
            phase = 3;
        }
        else if (checkpoint >= phase2Start)
        {
            phase = 2;
        }
        else
        {
            phase = 1;
        }
        Debug.Log("Phase: " + phase);
        playerController.MoveToCheckPoint(checkpoints[currentCheckpoint].position);
        Debug.Log("Telling player to move to checkpoint: " + checkpoints[currentCheckpoint].position);
        SongTransition();
        uiManager.ChangeCheckpointMarkers(phase);
        uiManager.ChangeTrackerMarker(phase);
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
        //StartCoroutine(EnableControls());
        //playerController.move = true;
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
        int curPosition;
        songTimeline.getTimelinePosition(out curPosition);
        if (curPosition == 8900) Debug.Log("Now");
        if (checkpoints.Count >= currentCheckpoint + 1 &&
            curPosition >= checkpoints[currentCheckpoint + 1].timelinePosition)
        {
            Debug.Log("Current Checkpoint: " + (currentCheckpoint + 1));
            //checkpoints[currentCheckpoint].vcam.enabled = false;
            currentCheckpoint++;
            //checkpoints[currentCheckpoint].vcam.enabled = true;
            uiManager.ChangeCheckpointMarkers(phase);
            uiManager.ChangeTrackerMarker(phase);
        }

        if (
                (playerController.speedIndex < 1 && 
                curPosition >= checkpoints[8].timelinePosition) ||
                (playerController.speedIndex < 2 &&
                curPosition >= checkpoints[14].timelinePosition)
            )
        {
            playerController.IncreaseSpeed();
        }
        else if (
                (playerController.speedIndex > 0 && 
                curPosition <= checkpoints[8].timelinePosition) ||
                (playerController.speedIndex > 1 &&
                curPosition <= checkpoints[14].timelinePosition)
            )
        {
            playerController.DecreaseSpeed();
        }

        if (currentCheckpoint == 1)
        {
            uiManager.ChangeCheckpointMarkers(1);
            uiManager.ChangeTrackerMarker(1);
            checkpoints[currentCheckpoint].vcam.enabled = true;
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
