using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void toUserLoad()
    {
        SceneManager.LoadScene("UserLoad");
    }

    public void toRobby()
    {
        SceneManager.LoadScene("Robby");
    }

    public void toPlay()
    {
        SceneManager.LoadScene("Play");
    }

    public void toTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void toSample()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
