using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public GameObject Respawn;
    public GameObject[] Charater;
    public GameObject[] Spikes;
    public Player player;
    public bool pause;
    public GameObject gameOver;
    public GameObject pauseWindow;
    public Text Coins;
    public Text Score;
    public Text EndCoin;
    public Text EndScore;
    public float GameSpeed;
    public float NowScore;
    public LoadData loaddata;

    void Awake()
    {
        loaddata = FindObjectOfType<LoadData>();
        Instantiate(Charater[int.Parse(loaddata.MyUseItem[0].CharaNo)], Respawn.transform.position, Charater[int.Parse(loaddata.MyUseItem[0].CharaNo)].transform.rotation);
        player = FindObjectOfType<Player>();
        GameObject a = Instantiate(Spikes[Random.Range(0, Spikes.Length)]);
        a.SetActive(true);
        Coins.text = "0";
        NowScore = 0;
        GameSpeed = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            NowScore = NowScore + player.user.GetScoreLV() * Time.deltaTime;
            Scoring();
            GameSpeed = GameSpeed * (1f + Time.deltaTime * 0.005f);
            Time.timeScale = GameSpeed;
           
            
        }
    }
    public void GameOver()
    {
        try
        {
            Destroy(player.gameObject);
        }
        catch
        {

        }
        pause = true;
        gameOver.SetActive(true);
        EndCoin.text = Coins.text;
        EndScore.text = Score.text;
        player.user.GameOver(int.Parse(EndCoin.text), int.Parse(EndScore.text));
        GameSpeed = 1f;
        Pause();
    }
    
    public void SetPause()
    {
        pause = true;
        pauseWindow.SetActive(true);
        Pause();
    }

    public void OffPause()
    {
        pause = false;
        pauseWindow.SetActive(false);
        Time.timeScale = GameSpeed;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    
    public void ObtainCoin(int i)
    {
        Coins.text = (int.Parse(Coins.text) + i).ToString();
    }

    public void Scoring()
    {
        Score.text=((int)NowScore).ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    
    public void Jump()
    {
        try
        {
            player.JumpClick();
        }
        catch
        {

        }
    }

    public void Slide()
    {
        try
        {
            player.SlideDown();
        }
        catch { }
    }
    public void SlideExit()
    {
        try
        {
            player.SlideUp();
        }
        catch { }
    }
}
