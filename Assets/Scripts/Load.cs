using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public string levelToLoad;
    public void LoadScene()
    {
        var es3File = new ES3File("SaveFile.es3");
        SceneManager.LoadScene(levelToLoad);
    }

}
