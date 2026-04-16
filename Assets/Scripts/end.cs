using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
        Cursor.lockState = CursorLockMode.Confined;
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(7);
        Application.LoadLevel("Menu");
    }
}
