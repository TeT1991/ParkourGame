// Scripts/Load.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}