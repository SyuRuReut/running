using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Serialiazation<T>
{
    public Serialiazation(List<T> _target) => target = _target;
    public List<T> target;
}

[System.Serializable]
public class UserData
{
    public string useChara, Health, Score, Coin, Coins, BestScore;

    public  UserData(string _userChara, string _Health, string _Score, string _Coin, string _Coins, string _BestScore)
    {
        useChara = _userChara; Health = _Health; Score = _Score; Coin = _Coin; Coins = _Coins; BestScore = _BestScore;
    }
}

[System.Serializable]
public class CharacterData
{
    public string CharaNo, CharaName, Explain, Skill;

    public CharacterData(string _CharaNo, string _CharaName, string _Explain, string _Skill)
    {
        CharaNo = _CharaNo; CharaName = _CharaName; Explain = _Explain; Skill = _Skill;
    }
}

[System.Serializable]
public class CharaPurch
{
    public string CharaNo, Gold;
    public bool Purchase;

    public CharaPurch(string _CharaNo, bool _Purchase, string _Gold)
    {
        CharaNo = _CharaNo; Gold = _Gold; Purchase = _Purchase;
    }
}

[System.Serializable]
public class UseItem
{
    public string CharaNo, ItemNo1, ItemNo2, ItemNo3;

    public UseItem(string _CharaNo, string _ItemNo1, string _ItemNo2, string _ItemNo3)
    {
        CharaNo = _CharaNo;ItemNo1 = _ItemNo1;ItemNo2 = _ItemNo2;ItemNo3 = _ItemNo3;
    }

}

public class LoadData : MonoBehaviour
{
    public TextAsset UserInfo, CharacterInfo, CharaPurchInfo, UseItemInfo;
    public List<UserData> user, MyInfo;
    public List<CharacterData> chara, MyChara;
    public List<CharaPurch> purch, MyPurch;
    public List<UseItem> useitem, MyUseItem;
    public SceneLoader scene;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        string[] line = UserInfo.text.Substring(0, UserInfo.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            user.Add(new UserData(row[0], row[1], row[2], row[3], row[4],row[5]));
        }
        line = CharacterInfo.text.Substring(0, CharacterInfo.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            chara.Add(new CharacterData(row[0], row[1], row[2], row[3]));
        }
        line = CharaPurchInfo.text.Substring(0, CharaPurchInfo.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            Debug.Log(row[1]);
            purch.Add(new CharaPurch(row[0], row[1] == "TRUE", row[2]));
        }

        line = UseItemInfo.text.Substring(0, UseItemInfo.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            useitem.Add(new UseItem(row[0], row[1], row[2],row[3]));
        }

        Debug.Log(Application.persistentDataPath);
        Load();
        scene.toRobby();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        LoadInfo();
        LoadChara();
        LoadPurch();
        LoadUseItem();
    }
    public void LoadInfo()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/MyInfo.txt");
            MyInfo = JsonUtility.FromJson<Serialiazation<UserData>>(jdata).target;
        }
        catch
        {
            string jdata = JsonUtility.ToJson(new Serialiazation<UserData>(user));
            File.WriteAllText(Application.persistentDataPath + "/MyInfo.txt", jdata);
        }
    }
    public void LoadChara()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/MyChara.txt");
            MyChara = JsonUtility.FromJson<Serialiazation<CharacterData>>(jdata).target;
        }
        catch
        {
            string jdata = JsonUtility.ToJson(new Serialiazation<CharacterData>(chara));
            File.WriteAllText(Application.persistentDataPath + "/MyChara.txt", jdata);
        }
    }

    public void LoadPurch()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/MyPurch.txt");
            MyPurch = JsonUtility.FromJson<Serialiazation<CharaPurch>>(jdata).target;
        }
        catch
        {
            string jdata  = JsonUtility.ToJson(new Serialiazation<CharaPurch>(purch));
            File.WriteAllText(Application.persistentDataPath + "/MyPurch.txt", jdata);
        }
    }

    public void LoadUseItem()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/MyUseItem.txt");
            MyUseItem = JsonUtility.FromJson<Serialiazation<UseItem>>(jdata).target;
        }
        catch
        {
            string jdata  = JsonUtility.ToJson(new Serialiazation<UseItem>(useitem));
            File.WriteAllText(Application.persistentDataPath + "/MyUseItem.txt", jdata);
            Load();
        }
    }
    public void Save()
    {
        string jdata = JsonUtility.ToJson(new Serialiazation<UserData>(MyInfo));
        File.WriteAllText(Application.persistentDataPath + "/MyInfo.txt", jdata);
        jdata = JsonUtility.ToJson(new Serialiazation<CharacterData>(MyChara));
        File.WriteAllText(Application.persistentDataPath + "/MyChara.txt", jdata);
        jdata = JsonUtility.ToJson(new Serialiazation<CharaPurch>(MyPurch));
        File.WriteAllText(Application.persistentDataPath + "/MyPurch.txt", jdata);
        jdata = JsonUtility.ToJson(new Serialiazation<UseItem>(MyUseItem));
        File.WriteAllText(Application.persistentDataPath + "/MyUseItem.txt", jdata);
    }

    public int GetUseChara()
    {
        return int.Parse(MyInfo[0].useChara);
    }
    public int GetHealthLV()
    {
        return int.Parse(MyInfo[0].Health);
    }
    public int GetCoinLV()
    {
        return int.Parse(MyInfo[0].Coin);
    }
    public int GetScoreLV()
    {
        return int.Parse(MyInfo[0].Score);
    }

    public int GetCoins()
    {
        return int.Parse(MyInfo[0].Coins);
    }

    public int GetBestScore()
    {
        return int.Parse(MyInfo[0].BestScore);
    }

    public void HealthUp(int i)
    {
        if (GetCoins() >= GetHealthLV() * 100)
        {
            MyInfo[0].Coins = (GetCoins() - GetHealthLV() * 100).ToString();
            MyInfo[0].Health = (GetHealthLV() + i).ToString();
            Save();
        }
    }
    public void CoinUp(int i)
    {
        if (GetCoins() >= GetCoinLV() * 100)
        {
            MyInfo[0].Coins = (GetCoins() - GetCoinLV() * 100).ToString();
            MyInfo[0].Coin = (GetCoinLV() + i).ToString();
            Save();
        }
    }
    public void ScoreUp(int i)
    {
        if (GetCoins() >= GetScoreLV() * 100)
        {
            MyInfo[0].Coins = (GetCoins() - GetScoreLV() * 100).ToString();
            MyInfo[0].Score = (GetScoreLV() + i).ToString();
            Save();
        }
    }

    public void GameOver(int coin,int score)
    {
        MyInfo[0].Coins = (GetCoins() + coin).ToString();
        if (GetBestScore() < score)
        {
            MyInfo[0].BestScore = score.ToString();
        }
        Save();
    }
}
