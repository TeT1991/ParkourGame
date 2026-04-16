// Scripts/Sens.cs
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Sens : MonoBehaviour
{
    [SerializeField] private GameObject senMenu;
    [SerializeField] private GameObject player;

    private void Start()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnPausePressed += CloseSensMenu;
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnPausePressed -= CloseSensMenu;
    }

    private void CloseSensMenu()
    {
        senMenu.SetActive(false);
        var fpc = player.GetComponent<FirstPersonController>();
        if (fpc != null)
        {
            fpc.enabled = true;
            FirstPersonController.curlo = 0;
        }
    }
}