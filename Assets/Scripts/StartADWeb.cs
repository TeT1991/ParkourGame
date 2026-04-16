// Scripts/StartADWeb.cs
using UnityEngine;

public class StartADWeb : MonoBehaviour
{
    private void Start()
    {
        AdsManager.Instance.ShowFullscreenAd();
    }
}