using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{

    void Start()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "SplashScreen": 
                StartCoroutine(LoadSceneAfterDelay(6, "IntroCutscene"));
                break;
            case "IntroCutscene":
                StartCoroutine(LoadSceneAfterDelay(10.5f, "MainMenu"));
                break;
        }
    }

    public void LoadScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitToDesktop ()
    {
        Application.Quit();
    }

    public void ReloadScene ()
    {
        Scene x = SceneManager.GetActiveScene();
        SceneManager.LoadScene(x.name);
    }

    private IEnumerator LoadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
