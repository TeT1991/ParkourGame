// Scripts/Save.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (InputManager.Instance != null)
            InputManager.Instance.OnBackPressed += ReturnToMenu;
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnBackPressed -= ReturnToMenu;
    }

    private void ReturnToMenu()
    {
        AdsManager.Instance.ShowFullscreenAd();
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("Menu");
    }
}