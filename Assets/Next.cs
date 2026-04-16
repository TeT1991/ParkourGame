using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public GameObject Player;
    public GameObject nextfx;
    public GameObject Camera;

    void Start()
    {
        Player.SetActive(true);
        nextfx = GameObject.Find("2");
        Camera = GameObject.Find("FirstPersonCharacter");
        nextfx.SetActive(false);
    }

        void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            StartCoroutine(ExampleCoroutine());
            nextfx.SetActive(true);
            Camera.SetActive(false);
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameDistribution.Instance.ShowAd();
    }

}
