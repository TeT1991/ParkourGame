// Scripts/end.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    [SerializeField] private float delayBeforeMenu = 7f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        StartCoroutine(LoadMenuAfterDelay());
    }

    private IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMenu);
        SceneManager.LoadScene("Menu");
    }
}