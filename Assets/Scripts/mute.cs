// Scripts/mute.cs
using UnityEngine;

public class mute : MonoBehaviour
{
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private GameObject unmuteIcon;

    private const int MUTED = 1;
    private const int UNMUTED = 0;

    private void Start()
    {
        // Восстанавливаем состояние при загрузке сцены
        ApplyMuteState(PlayerPrefs.GetInt("mute", UNMUTED));
    }

    public void SetUnmuted()
    {
        PlayerPrefs.SetInt("mute", UNMUTED);
        ApplyMuteState(UNMUTED);
    }

    public void SetMuted()
    {
        PlayerPrefs.SetInt("mute", MUTED);
        ApplyMuteState(MUTED);
    }

    private void ApplyMuteState(int state)
    {
        bool isMuted = state == MUTED;
        AudioListener.volume = isMuted ? 0f : 1f;
        muteIcon.SetActive(!isMuted);
        unmuteIcon.SetActive(isMuted);
    }
}