// Scripts/GamePauseMenu.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GamePauseMenu : MonoBehaviour
{
    [Header("Панель паузы")]
    [SerializeField] private GameObject pausePanel;

    [Header("Подсказки ПК — отдельный Canvas")]
    [SerializeField] private GameObject hintsPCUI;

    [Header("Слайдер чувствительности")]
    [SerializeField] private Slider sensitivitySlider;

    [Header("Настройки чувствительности")]
    [SerializeField] private float minSensitivity = 0.05f;
    [SerializeField] private float maxSensitivity = 0.5f;

    private bool _isPaused = false;
    private const string SensKey = "save";

    public static GamePauseMenu Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            if (hintsPCUI != null)
                Destroy(hintsPCUI);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (hintsPCUI != null)
            DontDestroyOnLoad(hintsPCUI);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        pausePanel.SetActive(false);

        SetupSlider();
        ApplyUIForScene(SceneManager.GetActiveScene());

        if (InputManager.Instance != null)
            InputManager.Instance.OnBackPressed += TogglePause;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (InputManager.Instance != null)
            InputManager.Instance.OnBackPressed -= TogglePause;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyUIForScene(scene);

        if (_isPaused)
            Resume();

        bool isMenuScene = scene.name == "Menu";
        if (isMenuScene)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // На игровой сцене применяем сохранённую чувствительность к новому FirstPersonController
            float saved = PlayerPrefs.GetFloat(SensKey, 0.1f);
            ApplySensitivity(saved);
        }
    }

    private void ApplyUIForScene(Scene scene)
    {
        bool isMenuScene = scene.name == "Menu";

        pausePanel.SetActive(false);

        if (hintsPCUI != null)
            hintsPCUI.SetActive(!isMenuScene && !IsMobile());
    }

    private void SetupSlider()
    {
        if (sensitivitySlider == null) return;

        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;

        float saved = PlayerPrefs.GetFloat(SensKey, 0.1f);
        sensitivitySlider.value = saved;
        ApplySensitivity(saved);

        sensitivitySlider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        ApplySensitivity(value);
        PlayerPrefs.SetFloat(SensKey, value);
    }

    private void ApplySensitivity(float value)
    {
        if (MobileTouchInput.Instance != null)
            MobileTouchInput.Instance.SetLookSensitivity(value);

        var fpc = FindObjectOfType<FirstPersonController>();
        if (fpc != null)
            fpc.SetSensitivity(value);
    }

    public void TogglePause()
    {
        if (SceneManager.GetActiveScene().name == "Menu") return;

        if (_isPaused) Resume();
        else Pause();
    }

    private void Pause()
    {
        _isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;

        var fpc = FindObjectOfType<FirstPersonController>();
        if (fpc != null) fpc.enabled = false;
    }

    public void Resume()
    {
        _isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        if (!IsMobile())
            Cursor.lockState = CursorLockMode.Locked;

        var fpc = FindObjectOfType<FirstPersonController>();
        if (fpc != null) fpc.enabled = true;
    }

    public void ReturnToMenu()
    {
        Resume();
        if (AdsManager.Instance != null)
            AdsManager.Instance.ShowFullscreenAd();
        SceneManager.LoadScene("Menu");
    }

    public void LoadScene(string sceneName)
    {
        Resume();
        if (AdsManager.Instance != null)
            AdsManager.Instance.ShowFullscreenAd();
        SceneManager.LoadScene(sceneName);
    }

    private bool IsMobile()
    {
        if (MobileTouchInput.Instance != null)
            return MobileTouchInput.Instance.IsMobile;

#if UNITY_EDITOR
        return false;
#else
            return Input.touchSupported && SystemInfo.deviceType == DeviceType.Handheld;
#endif
    }
}