// Scripts/off.cs
using System.Collections;
using UnityEngine;

public class off : MonoBehaviour
{
    [SerializeField] private float delay = 0.9f;

    private void Start()
    {
        StartCoroutine(DisableAfterDelay());
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}