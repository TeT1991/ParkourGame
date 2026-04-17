// Editor/EnableFPSController.cs
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class EnableFPSController : EditorWindow
{
    [MenuItem("Tools/Enable FPSController in all scenes")]
    public static void EnableInAllScenes()
    {
        string currentScene = EditorSceneManager.GetActiveScene().path;

        // Ищем только в папке Scenes
        string[] allScenes = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        int fixedCount = 0;

        foreach (string guid in allScenes)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            if (path.Contains("Menu")) continue;

            EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            Scene scene = EditorSceneManager.GetActiveScene();

            // Ищем все объекты включая выключенные
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "FPSController" && !obj.activeSelf &&
                    obj.scene == scene &&
                    obj.hideFlags == HideFlags.None)
                {
                    obj.SetActive(true);
                    EditorSceneManager.MarkSceneDirty(scene);
                    Debug.Log($"[EnableFPSController] Включён в сцене: {path}");
                    fixedCount++;
                    break;
                }
            }

            EditorSceneManager.SaveScene(scene);
        }

        EditorSceneManager.OpenScene(currentScene, OpenSceneMode.Single);
        Debug.Log($"[EnableFPSController] Готово. Исправлено сцен: {fixedCount}");
    }
}