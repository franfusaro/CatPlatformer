using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float LevelExitSlowMotionFactor = 0.2f;


    public void LoadMainMenu()
    {
        var scenePersist = FindObjectOfType<ScenePersist>();
        if (scenePersist != null)
        {
            Destroy(scenePersist.gameObject);
            scenePersist.gameObject.SetActive(false);
        }
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIndex);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel()
    {
        Time.timeScale = LevelExitSlowMotionFactor;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Destroy(FindObjectOfType<ScenePersist>().gameObject);
        FindObjectOfType<ScenePersist>().gameObject.SetActive(false);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadYouLoseScene()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
