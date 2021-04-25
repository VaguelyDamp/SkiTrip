using UnityEngine;

class CheckpointData : MonoBehaviour
{
    public CheckpointData(int time, float trackerPosition)
    {
        this.timelinePosition = time;
        this.trackerPosition = trackerPosition;
    }

    public int timelinePosition;
    public Vector2 position;
    public float trackerPosition;
}

