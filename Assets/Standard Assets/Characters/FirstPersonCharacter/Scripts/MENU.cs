using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU : MonoBehaviour
{
    // Start is called before the first frame update


    private void Start()
    {
        
    }


    public void Next(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
