using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Sens : MonoBehaviour
{
    public GameObject SenMenu;
    public GameObject Player;

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SenMenu.SetActive(false);
            Player.GetComponent<FirstPersonController>().enabled = true;
            FirstPersonController.curlo = 0;
        }
    }
}