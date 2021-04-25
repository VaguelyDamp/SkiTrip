using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject confirmationPanel;
    public Text confirmationText;
    public Button confirmationButton;

    private SceneController sceneController;

    void Start ()
    {
        sceneController = GameObject.Find("SceneController")
            .GetComponent<SceneController>();
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

}
