// Scripts/MobileTouchInput.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MobileTouchInput : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float moveSensitivity = 1f;

    [Header("Настройки камеры")]
    [SerializeField] private float lookSensitivity = 0.1f;

    [Header("Настройки прыжка")]
    [SerializeField] private float maxTapTime = 0.2f;

    [Header("Мобильный UI")]
    [SerializeField] private GameObject mobileUI;

    [Header("Тестирование")]
    [SerializeField] private bool forceMobileUI = false;

    private bool _isTouchDevice = false;
    private int _moveFingerID = -1;
    private int _lookFingerID = -1;
    private Vector2 _moveStartPos;
    private float _lookTouchStartTime = 0f;
    private float _moveTouchStartTime = 0f;

    public static MobileTouchInput Instance { get; private set; }
    public bool IsMobile => _isTouchDevice;

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

        _moveFingerID = -1;
        _lookFingerID = -1;

        ResetAxes();
    }

    public void SetLookSensitivity(float value)
    {
        lookSensitivity = value;
    }

    private IEnumerator JumpCoroutine()
    {
        SafeSetButtonDown("Jump");
        yield return null;
        SafeSetButtonUp("Jump");
    }

    private void Update()
    {
        if (!_isTouchDevice) return;
        ProcessTouches();
    }

    private void ProcessTouches()
    {
        float halfScreen = Screen.width * 0.5f;

        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (touch.position.x < halfScreen && _moveFingerID == -1)
                    {
                        _moveFingerID = touch.fingerId;
                        _moveStartPos = touch.position;
                        _moveTouchStartTime = Time.time;
                    }
                    else if (touch.position.x >= halfScreen && _lookFingerID == -1)
                    {
                        _lookFingerID = touch.fingerId;
                        _lookTouchStartTime = Time.time;
                    }
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (touch.fingerId == _moveFingerID)
                        HandleMovement(touch.position);

                    if (touch.fingerId == _lookFingerID)
                        HandleLook(touch.deltaPosition);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == _moveFingerID)
                    {
                        if (Time.time - _moveTouchStartTime <= maxTapTime)
                            StartCoroutine(JumpCoroutine());

                        _moveFingerID = -1;
                        SafeSetAxis("Horizontal", 0f);
                        SafeSetAxis("Vertical", 0f);
                    }

                    if (touch.fingerId == _lookFingerID)
                    {
                        if (Time.time - _lookTouchStartTime <= maxTapTime)
                            StartCoroutine(JumpCoroutine());

                        _lookFingerID = -1;
                        SafeSetAxis("Mouse X", 0f);
                        SafeSetAxis("Mouse Y", 0f);
                    }
                    break;
            }
        }
    }

    private void HandleMovement(Vector2 currentPos)
    {
        Vector2 delta = currentPos - _moveStartPos;
        float screenScale = Screen.height * 0.2f;
        float horizontal = Mathf.Clamp(delta.x / screenScale, -1f, 1f) * moveSensitivity;
        float vertical = Mathf.Clamp(delta.y / screenScale, -1f, 1f) * moveSensitivity;

        SafeSetAxis("Horizontal", horizontal);
        SafeSetAxis("Vertical", vertical);
    }

    private void HandleLook(Vector2 delta)
    {
        SafeSetAxis("Mouse X", delta.x * lookSensitivity);
        SafeSetAxis("Mouse Y", delta.y * lookSensitivity);
    }

    private void ResetAxes()
    {
        SafeSetAxis("Horizontal", 0f);
        SafeSetAxis("Vertical", 0f);
        SafeSetAxis("Mouse X", 0f);
        SafeSetAxis("Mouse Y", 0f);
    }

    // Защита от исключения на standalone платформе
    private void SafeSetAxis(string name, float value)
    {
        try { CrossPlatformInputManager.SetAxis(name, value); }
        catch { /* standalone mode — игнорируем */ }
    }

    private void SafeSetButtonDown(string name)
    {
        try { CrossPlatformInputManager.SetButtonDown(name); }
        catch { /* standalone mode — игнорируем */ }
    }

    private void SafeSetButtonUp(string name)
    {
        try { CrossPlatformInputManager.SetButtonUp(name); }
        catch { /* standalone mode — игнорируем */ }
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