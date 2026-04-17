// Scripts/Next.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Next : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject nextFX;
    [SerializeField] private GameObject fpsCamera;

    private bool _triggered = false;

    private void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        if (fpsCamera == null) fpsCamera = GameObject.Find("FirstPersonCharacter");

        if (player != null)
            player.SetActive(true);

        if (nextFX != null)
            nextFX.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;

        if (other.CompareTag("Player"))
        {
            _triggered = true;

            var fpc = other.GetComponent<FirstPersonController>();
            if (fpc != null) fpc.enabled = false;

            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            if (nextFX != null)
                nextFX.SetActive(true);

            // Открываем следующий уровень
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            LevelProgress.MaxUnlockedLevel = nextIndex;

            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        if (AdsManager.Instance != null)
            AdsManager.Instance.ShowFullscreenAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}