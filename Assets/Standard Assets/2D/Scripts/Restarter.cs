using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Restarter : MonoBehaviour
    {


    public GameObject nextfx;
    public GameObject Camera;

    void Start()
    {
        nextfx = GameObject.Find("1");
        Camera = GameObject.Find("FirstPersonCharacter");
        nextfx.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
            StartCoroutine(ExampleCoroutine());
            nextfx.SetActive(true);
            Camera.SetActive(false);
        }

        }


    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(Application.loadedLevel);
    }

}
