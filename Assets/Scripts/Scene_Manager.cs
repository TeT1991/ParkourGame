// Scripts/Scene_Manager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    private const string SaveKey = "Saved";

    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadSavedScene()
    {
        int savedScene = PlayerPrefs.GetInt(SaveKey, 0);
        if (savedScene != 0)
            SceneManager.LoadSceneAsync(savedScene);
    }

    public void SaveAndExit()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(SaveKey, currentIndex);
        PlayerPrefs.Save();

        // Для YG2 замени PlayerPrefs на: YG2.SavesData.saves.savedScene = currentIndex; YG2.SaveProgress();

        SceneManager.LoadSceneAsync(0);
    }

    public void NextScene()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadSceneAsync(nextIndex);
    }
}