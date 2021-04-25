using UnityEngine;

class CheckpointData : MonoBehaviour
{
    public CheckpointData(int time, float trackerPosition)
    {
        this.timelinePosition = time;
        this.trackerPosition = trackerPosition;
    }

    public int timelinePosition;
    public Vector3 position;
    public float trackerPosition;
    public Cinemachine.CinemachineVirtualCamera vcam;
}

