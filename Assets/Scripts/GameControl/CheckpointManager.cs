using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CheckpointManager : MonoBehaviour
{
    public float deathTime = 3f;
    public float fadeInTime = 1f;

    public FMOD.Studio.EventInstance songTimeline;
    [FMODUnity.EventRef]
    public string startMusic;

    IDictionary<int, CheckpointData> checkpoints = new Dictionary<int, CheckpointData>()
    {
        {1, new CheckpointData(9047)},
        {2, new CheckpointData(32997)},
        {3, new CheckpointData(56999)},
        {4, new CheckpointData(69081)},
        {5, new CheckpointData(81059)},
        {6, new CheckpointData(105098)},
        {7, new CheckpointData(147065)}
    };

    public int currentCheckpoint = 1;

    public float Remap(float value, float fromStart, float fromEnd, float toStart, float toEnd)
    {
        float t = (value - fromStart) / (fromEnd - fromStart);
        return t * (toEnd - toStart) + toStart;
    }

    void Start ()
    {
        songTimeline = FMODUnity.RuntimeManager.CreateInstance(startMusic);
        songTimeline.start();
    }

    public void LoadCheckpoint (int checkpoint)
    {
        currentCheckpoint = checkpoint;
        SongTransition();
    }

    public void OnDeath ()
    {
        songTimeline.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(UnDie());
    }

    private IEnumerator UnDie ()
    {
        yield return new WaitForSeconds(deathTime);
        songTimeline.start();
        SongTransition();
    }

    private void SongTransition ()
    {
        songTimeline.setTimelinePosition(
            checkpoints[currentCheckpoint]
            .timelinePosition - (int)(fadeInTime * 1000));
        StartCoroutine(FadeSong());
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
        if (checkpoints.Count >= currentCheckpoint + 1 &&
            curPosition >= checkpoints[currentCheckpoint + 1].timelinePosition)
        {
            currentCheckpoint++;
            Debug.Log("Current Checkpoint: " + currentCheckpoint);
        }
    }
}
