using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class off : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
            yield return new WaitForSeconds(0.9f);
        gameObject.SetActive(false);
    }
    
}
