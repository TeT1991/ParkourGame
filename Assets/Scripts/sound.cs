// Scripts/sound.cs
using UnityEngine;

public class sound : MonoBehaviour
{
    public bool soundFXNext = false;
    public bool soundFXHit = false;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource nextSound;

    private void Update()
    {
        // Было: if (soundfxhit = true) — это ПРИСВАИВАНИЕ, звук играл всегда!
        if (soundFXHit)
        {
            hitSound.Play();
            soundFXHit = false;
        }

        if (soundFXNext)
        {
            nextSound.Play();
            soundFXNext = false;
        }
    }
}