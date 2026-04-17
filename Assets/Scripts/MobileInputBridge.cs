// Scripts/MobileInputBridge.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MobileInputBridge : MonoBehaviour
{
    [Header("Джойстики")]
    [SerializeField] private Joystick leftJoystick;
    [SerializeField] private Joystick rightJoystick;

    [Header("Настройки камеры")]
    [SerializeField] private float lookSensitivity = 3f;

    [Header("Мобильный UI")]
    [SerializeField] private GameObject mobileUI;

    [Header("Тестирование")]
    [SerializeField] private bool forceMobileUI = false;

    private bool _jumpPressed = false;
    private bool _isTouchDevice = false;

    public static MobileInputBridge Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _isTouchDevice = forceMobileUI || IsTouchDevice();

        if (mobileUI != null)
        {
            DontDestroyOnLoad(mobileUI);
            mobileUI.SetActive(false);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mobileUI == null) return;

        bool isMenuScene = scene.name == "Menu";
        mobileUI.SetActive(_isTouchDevice && !isMenuScene);
    }

    public void OnJumpButtonPressed() => _jumpPressed = true;

    private void Update()
    {
        if (!_isTouchDevice) return;

        if (leftJoystick != null)
        {
            CrossPlatformInputManager.SetAxis("Horizontal", leftJoystick.Horizontal);
            CrossPlatformInputManager.SetAxis("Vertical", leftJoystick.Vertical);
        }

        if (rightJoystick != null)
        {
            CrossPlatformInputManager.SetAxis("Mouse X", rightJoystick.Horizontal * lookSensitivity);
            CrossPlatformInputManager.SetAxis("Mouse Y", rightJoystick.Vertical * lookSensitivity);
        }

        if (_jumpPressed)
        {
            CrossPlatformInputManager.SetButtonDown("Jump");
            _jumpPressed = false;
        }
    }

    private bool IsTouchDevice()
    {
#if UNITY_EDITOR
        return false;
#else
            return Input.touchSupported && SystemInfo.deviceType == DeviceType.Handheld;
#endif
    }
}