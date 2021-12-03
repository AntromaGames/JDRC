using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{


    public void QuitGame()
    {

        Application.Quit();
    }
    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}
