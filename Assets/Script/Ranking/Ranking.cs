using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.Networking;

public class Ranking : MonoBehaviour
{
    // Start is called before the first frame update
    private string RankingUrl;
    private string num;//몇번 스테이지를 골랐는지
    public GameObject sentence;//몇등,ID,죽은횟수
    public GameObject Content;
    private void Awake()
    {
        RankingUrl="bjh3311.cafe24.com/Ranking.php";
    }

    // Update is called once per frame
    public void Back()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadRank(string stage)
    {
        if(stage=="All")
        {
            num="0";// 모든 Stage면 0
        }
        else if(stage=="Stage1")
        {
            num="1";
        }
        else if(stage=="Stage2")
        {
            num="2";
        }
        else if(stage=="Stage3")
        {
            num="3";
        }
        else if(stage=="Stage4")
        {
            num="4";
        }
        StartCoroutine("Load");
    }
    private IEnumerator Load()//DB에서  Stage 랭킹 불러오기
    {
        WWWForm form=new WWWForm();
        form.AddField("Stage",num);
        UnityWebRequest webRequest=UnityWebRequest.Post(RankingUrl,form);
        yield return webRequest.SendWebRequest();
        string[] temp=webRequest.downloadHandler.text.Split('^');
        int index=1;
        var item=Instantiate(sentence,new Vector3(0,0,0),Quaternion.identity);
        item.transform.SetParent(Content.transform);
        foreach(string i in temp)
        {
            if(i.Length!=0)
            {
                Debug.Log(i);
                index++;
            }

            
        }
    }
}
