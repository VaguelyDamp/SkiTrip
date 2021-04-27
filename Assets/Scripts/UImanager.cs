using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Sprite phase1Marker;
    public Sprite[] phase2Markers;
    public Sprite[] phase3Markers;

    public GameObject winScreen;
    public GameObject progressTracker;

    public GameObject sun;
    public Light sunLight;
    public Vector2 dawn;
    public Vector2 noon;
    public Vector2 dusk;

    public GameObject trackerMarkerGO;
    public Image trackerMarker;
    public float trackerMarkerStart;
    public float trackerMarkerEnd;
    public Sprite phase1Tracker;
    public Sprite phase2Tracker;
    public Sprite phase3Tracker;

    public Image fin;
    public Sprite phase1Fin;
    public Sprite phase2Fin;
    public Sprite phase3Fin;

    private float test = 0f;

    void Start ()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
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

    public void OnWin ()
    {
        progressTracker.SetActive(false);
        winScreen.SetActive(true);
    }

    private void OnDeath ()
    {
        //deathText.SetActive(true);
        //StartCoroutine(HideDeathText());
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
                Debug.Log("Ballz");
                confirmationButton.onClick.AddListener(QuitToDesktop);
                break;
            default:
                break;
        }
    }

    public void ChangeCheckpointMarkers (int phase)
    {
        Sprite[] images = null;
        if (phase == 1)
        {
            foreach (GameObject marker in checkpointMarkers)
            {
                Image markerImage = marker.GetComponent<Image>();
                markerImage.sprite = phase1Marker;
            }
        }
        if (phase == 2) images = phase2Markers;
        if (phase == 3) images = phase3Markers;
        if (images == null) return;
        foreach (GameObject marker in checkpointMarkers)
        {
            Image markerImage = marker.GetComponent<Image>();
            int rand = Random.Range(0, images.Length);
            markerImage.sprite = images[rand];
        }
    }

    public void ChangeTrackerMarker (int phase)
    {
        switch (phase)
        {
            case 1:
                fin.sprite = phase1Fin;
                trackerMarker.sprite = phase1Tracker;
                break;
            case 2:
                fin.sprite = phase2Fin;
                trackerMarker.sprite = phase2Tracker;
                break;
            case 3:
                fin.sprite = phase3Fin; 
                trackerMarker.sprite = phase3Tracker;
                break;
            default:
                break;
        }
    }

    void Update ()
    {
        test++;
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
        trackerMarkerGO.GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(
                Remap(
                    checkpointManager.progressThroughGame,
                    0f,
                    1f,
                    trackerMarkerStart,
                    trackerMarkerEnd
                ), -19f);
        MoveSun (checkpointManager.progressThroughGame);
    }

    private void MoveSun (float progress)
    {
        Vector2 from;
        Vector2 to;
        float subProgress;
        if (progress >= 5f)
        {
            subProgress = Remap(
                progress,
                0.5f,
                1f,
                0f,
                1f
            );
            from = noon;
            to = dusk;
        }
        else
        {
            subProgress = Remap(
                progress,
                0f,
                0.5f,
                0f,
                1f
            );
            from = dawn;
            to = noon;
        }

        sun.transform.eulerAngles = new Vector3(Remap(
            subProgress,
            0f,
            1f,
            from.x,
            to.x
        ), Remap(
            subProgress,
            0f,
            1f,
            from.y,
            to.y
        ),
            0f
        );
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

    public void QuitToDesktop ()
    {
        sceneController.QuitToDesktop();
    }

    public float Remap(float value, float fromStart, float fromEnd, float toStart, float toEnd)
    {
        float t = (value - fromStart) / (fromEnd - fromStart);
        return t * (toEnd - toStart) + toStart;
    }

}
