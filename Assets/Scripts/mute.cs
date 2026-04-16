using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mute : MonoBehaviour
{

    public GameObject mutepng;
    public GameObject mutepng1;
    public void MuteToggl()
    {
        PlayerPrefs.SetInt("mute",  0);
        AudioListener.volume = 1;
            mutepng.SetActive(true);
            mutepng1.SetActive(false);
    }

    public void MuteToggl1()
    {
        PlayerPrefs.SetInt("mute", 1);
        AudioListener.volume = 0;
        mutepng.SetActive(false);
        mutepng1.SetActive(true);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("mute") == 0)
        {
            AudioListener.volume = 1;
            mutepng.SetActive(true);
            mutepng1.SetActive(false);
        }

        if (PlayerPrefs.GetInt("mute") == 1)
        {
            AudioListener.volume = 0;
            mutepng.SetActive(false);
            mutepng1.SetActive(true);
        }
    }
}
