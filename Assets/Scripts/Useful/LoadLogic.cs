using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLogic : MonoBehaviour
{
    public static void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneName);
    }
    public static void LoadNextInBuild()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void LoadPreviousInBuild()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public static void LoadSceneByNumber(int sceneNumber)
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneNumber);
    }
    public static void ReloadScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void LoadSceneByPlayerPrefInt(string tag)
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(PlayerPrefs.GetInt(tag));
    }
    public static void LoadSceneByPlayerPrefString(string tag)
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(PlayerPrefs.GetString(tag));
    }
    public static void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Player Quit");
    }
    public static void OpenLink(string url)
    {
        Application.OpenURL(url);
    }

    public void UnLoadObject(GameObject gameobject)
    {
        gameobject.SetActive(false);
    }

    public void LoadObject(GameObject gameobject)
    {
        gameobject.SetActive(true);
    }
}