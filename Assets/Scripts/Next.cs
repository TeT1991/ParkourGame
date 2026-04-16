// Scripts/Next.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject nextFX;
    [SerializeField] private GameObject fpsCamera;

    private void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        if (fpsCamera == null) fpsCamera = GameObject.Find("FirstPersonCharacter");

        player.SetActive(true);

        if (nextFX != null)
            nextFX.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nextFX != null)
                nextFX.SetActive(true);

            // ”брали fpsCamera.SetActive(false) Ч это ломало камеру на следующей сцене
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        AdsManager.Instance.ShowFullscreenAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}