// Scripts/Next.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject nextFX;     // объект с именем "2"
    [SerializeField] private GameObject fpsCamera;  // "FirstPersonCharacter"

    private void Start()
    {
        player.SetActive(true);

        if (nextFX == null) nextFX = GameObject.Find("2");
        if (fpsCamera == null) fpsCamera = GameObject.Find("FirstPersonCharacter");

        nextFX.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nextFX.SetActive(true);
            fpsCamera.SetActive(false);
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