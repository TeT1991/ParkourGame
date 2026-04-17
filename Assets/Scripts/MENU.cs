// MENU.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU : MonoBehaviour
{
    public void Next(string sceneName)
    {
        // Проверяем открыт ли уровень по индексу сцены
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(GetScenePath(sceneName));

        if (buildIndex >= 0 && !LevelProgress.IsLevelUnlocked(buildIndex))
        {
            Debug.Log($"[MENU] Уровень {sceneName} заблокирован");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void Next(int buildIndex)
    {
        if (!LevelProgress.IsLevelUnlocked(buildIndex))
        {
            Debug.Log($"[MENU] Уровень с индексом {buildIndex} заблокирован");
            return;
        }

        SceneManager.LoadScene(buildIndex);
    }

    private string GetScenePath(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (path.Contains("/" + sceneName + ".unity"))
                return path;
        }
        return "";
    }
}