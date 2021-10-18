using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
     private static int m_mainMenuIndex = 1;


    public static void LoadLevel(string levelName)
    {
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogWarning("LEVEL LOADER ERROR: Tratas de cargar un nivel que no existe");
        }

    }
    public static void LoadLevel(int levelIndex)
    {

        if (levelIndex >= 0 && levelIndex <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogWarning("LEVEL LOADER ERROR: Tratas de cargar un nivel que no existe");
        }

    }

    public static void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadLevel(currentScene.buildIndex);
    }

    public static void LoadNextLevel()
    {

        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        nextSceneIndex = Mathf.Clamp(nextSceneIndex, m_mainMenuIndex, nextSceneIndex);
        LoadLevel(nextSceneIndex);

    }
    public static void LoadMainMenuLevel()
    {
        LoadLevel(m_mainMenuIndex);
    }


}
