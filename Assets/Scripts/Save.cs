using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown("backspace"))
        {
            GameDistribution.Instance.ShowAd();
            Application.LoadLevel("Menu");
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

}
