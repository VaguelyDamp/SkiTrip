using UnityEngine;

class CheckpointData : MonoBehaviour
{
    public CheckpointData(int time)
    {
        this.timelinePosition = time;
    }

    public int timelinePosition;
    public Vector2 position;
    public Cinemachine.CinemachineVirtualCamera vcam;
}

