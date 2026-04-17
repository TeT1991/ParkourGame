// Scripts/Save.cs
using UnityEngine;

public class Save : MonoBehaviour
{
    private void Start()
    {
        // Лок курсора только на десктопе
        if (SystemInfo.deviceType != DeviceType.Handheld)
            Cursor.lockState = CursorLockMode.Locked;
    }
}