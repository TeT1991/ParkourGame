// Scripts/InputManager.cs
using UnityEngine;

/// <summary>
/// Центральное место для всего ввода.
/// Для мобилки: добавь публичные методы OnBackButtonPressed() / OnPauseButtonPressed()
/// и повесь их на UI-кнопки в Canvas.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // События, на которые подписываются другие скрипты
    public System.Action OnPausePressed;
    public System.Action OnBackPressed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("[InputManager] Backspace нажат. Подписчиков: " + (OnBackPressed?.GetInvocationList().Length ?? 0));
            OnBackPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            OnPausePressed?.Invoke();
    }

    // Мобилка — повесить на UI кнопки
    public void OnBackButtonPressed() => OnBackPressed?.Invoke();
    public void OnPauseButtonPressed() => OnPausePressed?.Invoke();
}