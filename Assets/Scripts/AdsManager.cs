// Scripts/AdsManager.cs
using UnityEngine;

/// <summary>
/// Единая точка для показа рекламы.
/// Чтобы подключить YG2 — раскомментируй using YG; и замени тело ShowFullscreenAd().
/// Чтобы подключить GameDistribution — замени на GameDistribution.Instance.ShowAd().
/// </summary>
public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance { get; private set; }

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

    public void ShowFullscreenAd()
    {
        // --- GameDistribution (текущий вариант) ---
        // GameDistribution.Instance.ShowAd();

        // --- YG2 (раскомментируй когда подключишь плагин) ---
        // YG2.Adv.ShowFullscreenAdv();

        Debug.Log("[AdsManager] ShowFullscreenAd вызван (заглушка)");
    }
}