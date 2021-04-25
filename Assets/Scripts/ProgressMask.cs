using UnityEngine;
using UnityEngine.UI;

public class ProgressMask : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    private RectTransform rectTransform;
    private Vector3 farLeft;
    private Vector3 farRight;

    public DonutAss slider;

    private void Awake ()
    {
        rectTransform = GetComponent<RectTransform>();
        farLeft = rectTransform.position - new Vector3(rectTransform.rect.width, 0f);
        farRight = rectTransform.position;
    }

    private void Start ()
    {
        checkpointManager = GameObject.Find("CheckpointManager")
            .GetComponent<CheckpointManager>();
    }

    private void Update ()
    {
        
        HandleSliderChanged(checkpointManager.progressThroughGame);
    }

    private void HandleSliderChanged(float value)
    {
        slider.Fill = value;
    }
}