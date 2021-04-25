using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject confirmationPanel;
    public Text confirmationText;
    public Button confirmationButton;

    public GameObject deathText;

    private GameController gameController;
    private CheckpointManager checkpointManager;
    private SceneController sceneController;

    public GameObject[] checkpointMarkers;
    public Sprite phase1MarkerLocked;
    public Sprite phase1MarkerCleared;
    public Sprite[] phase2MarkersLocked;
    public Sprite[] phase2MarkersCleared;
    public Sprite[] phase3MarkersLocked;
    public Sprite[] phase3MarkersCleared;

    public GameObject trackerMarkerGO;
    public Image trackerMarker;
    public float trackerMarkerStart;
    public float trackerMarkerEnd;
    public Sprite phase1Tracker;
    public Sprite phase2Tracker;
    public Sprite phase3Tracker;

    private float test = 0f;

    void Start ()
    {
        sceneController = GameObject.Find("SceneController")
            .GetComponent<SceneController>();
        gameController = GameObject.Find("GameController")
            .GetComponent<GameController>();
        gameController.OnDeath += OnDeath;
        checkpointManager = GameObject.Find("CheckpointManager")
            .GetComponent<CheckpointManager>();
        ChangeCheckpointMarkers(1);
        trackerMarkerGO = GameObject.Find("TrackerMarker");
    }

    private void OnDeath ()
    {
        deathText.SetActive(true);
        StartCoroutine(HideDeathText());
    }

    private IEnumerator HideDeathText ()
    {
        yield return new WaitForSeconds(
            GameObject.Find("CheckpointManager")
            .GetComponent<CheckpointManager>()
            .deathTime
        );
        deathText.SetActive(false);
    }

    public void PromptConfirmNegative (string action)
    {
        confirmationPanel.SetActive(true);
        confirmationText.text = "Are you sure you want to "
            + action + "?";
        switch (action)
        {
            case "quit to menu":
                confirmationButton.onClick.AddListener(GoToMenu);
                break;
            case "quit to desktop":
                confirmationButton.onClick.AddListener(QuitToDesktop);
                break;
            default:
                break;
        }
    }

    public void ChangeCheckpointMarkers (int phase)
    {
        int currentCheckpoint = checkpointManager.currentCheckpoint;
        int i = 0;
        switch (phase)
        {
            case 1:
                foreach (GameObject marker in checkpointMarkers)
                {
                    Image markerImage = marker.GetComponent<Image>();
                    if (i < currentCheckpoint)
                    {
                        markerImage.sprite = phase1MarkerCleared;
                        i++;
                    }
                    else
                    {
                        markerImage.sprite = phase1MarkerLocked;
                    }
                }
                break;
            case 2:
                foreach (GameObject marker in checkpointMarkers)
                {
                    Image markerImage = marker.GetComponent<Image>();
                    int rand = Random.Range(0, phase2MarkersLocked.Length);
                    if (i < currentCheckpoint)
                    {
                        markerImage.sprite = phase2MarkersCleared[rand];
                        i++;
                    }
                    else
                    {
                        markerImage.sprite = phase2MarkersLocked[rand];
                    }
                }
                break;
            case 3:
                foreach (GameObject marker in checkpointMarkers)
                {
                    Image markerImage = marker.GetComponent<Image>();
                    int rand = Random.Range(0, phase3MarkersLocked.Length);
                    if (i < currentCheckpoint)
                    {
                        markerImage.sprite = phase3MarkersCleared[rand];
                        i++;
                    }
                    else
                    {
                        markerImage.sprite = phase3MarkersLocked[rand];
                    }
                }
                break;
            default:
                break;
        }
    }

    public void ChangeTrackerMarker (int phase)
    {
        switch (phase)
        {
            case 1:
                trackerMarker.sprite = phase1Tracker;
                break;
            case 2:
                trackerMarker.sprite = phase2Tracker;
                break;
            case 3:
                trackerMarker.sprite = phase3Tracker;
                break;
            default:
                break;
        }
    }

    void Update ()
    {
        test++;
        /*trackerMarkerGO.GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(
                Remap(
                    checkpointManager.progressThroughGame,
                    0f,
                    1f,
                    trackerMarkerStart,
                    trackerMarkerEnd
                ), -139.33f);*/
    }

    public void BackOut ()
    {
        confirmationButton.onClick.RemoveAllListeners();
        confirmationPanel.SetActive(false);
    }

    private void GoToMenu  ()
    {
        sceneController.LoadScene("MainMenu");
    }

    private void QuitToDesktop ()
    {
        sceneController.QuitToDesktop();
    }

    public float Remap(float value, float fromStart, float fromEnd, float toStart, float toEnd)
    {
        float t = (value - fromStart) / (fromEnd - fromStart);
        return t * (toEnd - toStart) + toStart;
    }

}
