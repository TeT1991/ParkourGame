using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public bool soundfxnext = false;
    public bool soundfxhit = false;
    public AudioSource Hit;
    public AudioSource Next;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (soundfxhit = true)
        {
            Hit.Play();
            soundfxhit = false;
        }

        if (soundfxnext = true)
        {
            Next.Play();
            soundfxnext = false;
        }
    }
}
