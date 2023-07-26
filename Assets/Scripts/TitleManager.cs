using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public SceneLoader scene;
    public GameObject quit;
    public bool OnWindow=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit.SetActive(true);
            OnWindow = true;
        }
        else if (Input.anyKeyDown&&!OnWindow)
        {
            scene.toUserLoad();
        }
    }

    public void Offquit()
    {
        quit.SetActive(false);
        OnWindow = false;
    }
    public void OffGame()
    {
        Application.Quit();
    }
}
