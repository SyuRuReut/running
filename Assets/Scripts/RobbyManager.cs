using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyManager : MonoBehaviour
{
    public LoadData loadData;
    public List<UserData> user;
    public List<CharacterData> chara;
    public List<CharaPurch> purch;
    public Text healthLV, coinLV, scoreLV, coins, needHealthUp, needCoinUp, needScoreUp, BestScore, InfoName, InfoSet, InfoAbility;
    public GameObject ChangeWindow, InfoWindow, SetWindow, FailSetWindow, BuyCharaWindow, quit;
    public GameObject[] Charas;
    public Sprite[] CharaSprites;
    public List<UseItem> use;
    public Image MainChara;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        loadData = FindObjectOfType<LoadData>();
        user = loadData.MyInfo;
        chara = loadData.MyChara;
        purch = loadData.MyPurch;
        use = loadData.MyUseItem;
        TextEdit();
        MainChara.sprite = CharaSprites[int.Parse(use[0].CharaNo)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit.SetActive(true);
        }
    }

    public void HealthUp()
    {
        loadData.HealthUp(1);
        TextEdit();
    }
    public void CoinUp()
    {
        loadData.CoinUp(1);
        TextEdit();
    }
    public void ScorehUp()
    {
        loadData.ScoreUp(1);
        TextEdit();
    }
    public void TextEdit()
    {
        healthLV.text = loadData.GetHealthLV().ToString();
        coinLV.text = loadData.GetCoinLV().ToString();
        scoreLV.text = loadData.GetScoreLV().ToString();
        coins.text = loadData.GetCoins().ToString();
        needHealthUp.text = (loadData.GetHealthLV() * 100).ToString();
        needCoinUp.text = (loadData.GetCoinLV() * 100).ToString();
        needScoreUp.text = (loadData.GetScoreLV() * 100).ToString();
        BestScore.text = loadData.GetBestScore().ToString();
    }

    public void OnChangeChara()
    {
        ChangeWindow.SetActive(true);
        for(int i =0; i<Charas.Length; i++)
        {
            Charas[i].SetActive(i < chara.Count);
            if (i < chara.Count)
            {
                if(!purch[i].Purchase)
                    Charas[i].GetComponentsInChildren<Text>()[0].text = purch[i].Gold+" 코인";
                else
                    Charas[i].GetComponentsInChildren<Text>()[0].text = "선택";
                Charas[i].GetComponentsInChildren<Text>()[1].text=chara[i].CharaName;
                Charas[i].GetComponentInChildren<Image>().sprite = CharaSprites[i];
            }
        }
    }

    public void OffChangeChara()
    {
        ChangeWindow.SetActive(false);
    }

    public void OnCharaInfo(int i)
    {
        InfoWindow.SetActive(true);
        InfoName.text = chara[i].CharaName;
        InfoSet.text = chara[i].Explain;
        InfoAbility.text = chara[i].Skill;
    }

    public void OffCharaInfo()
    {
        InfoWindow.SetActive(false);
    }

    public void SetChara(int i)
    {
        if (purch[i].Purchase)
        {
            use[0].CharaNo = i.ToString();
            MainChara.sprite = CharaSprites[int.Parse(use[0].CharaNo)];
            SetWindow.SetActive(true);
            loadData.Save();
        }
        else
        {
            if (loadData.GetCoins() >= int.Parse(purch[i].Gold))
            {
                user[0].Coins = (loadData.GetCoins() - int.Parse(purch[i].Gold)).ToString();
                purch[i].Purchase = true;
                OnChangeChara();
                loadData.Save();
                BuyCharaWindow.SetActive(true);             
            }
            else
            {
                FailSetWindow.SetActive(true);
            }
        }
    }

    public void offSet()
    {
        SetWindow.SetActive(false);
    }
    public void offBuy()
    {
        BuyCharaWindow.SetActive(false);
    }

    public void offFail()
    {
        FailSetWindow.SetActive(false);
    }
    public void Offquit()
    {
        quit.SetActive(false);
    }
    public void OffGame()
    {
        Application.Quit();
    }
}
