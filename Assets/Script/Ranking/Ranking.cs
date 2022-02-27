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
    public GameObject sentence;
    public GameObject[] Content;
    public GameObject[] Scroll;
    private void Awake()
    {
        RankingUrl="bjh3311.cafe24.com/Ranking.php";
        for(int i=0;i<=4;i++)
        {
            StartCoroutine("Load",i);
        }
    }
    // Update is called once per frame
    public void Back()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadRank(string stage)
    {
        foreach(GameObject temp in Scroll)
        {
            temp.SetActive(false);
        }
        if(stage=="All")
        {
            Scroll[0].SetActive(true);
        }
        else if(stage=="Stage1")
        {
            Scroll[1].SetActive(true);
        }
        else if(stage=="Stage2")
        {
            Scroll[2].SetActive(true);
        }
        else if(stage=="Stage3")
        {
            Scroll[3].SetActive(true);
        }
        else if(stage=="Stage4")
        {
            Scroll[4].SetActive(true);
        }
    }
    private IEnumerator Load(int num)//DB에서  Stage 랭킹 불러오기
    {
        WWWForm form=new WWWForm();
        form.AddField("Stage",num);
        UnityWebRequest webRequest=UnityWebRequest.Post(RankingUrl,form);
        yield return webRequest.SendWebRequest();
        string[] temp=webRequest.downloadHandler.text.Split('^');
        int index=1;
        foreach(string i in temp)
        {
            if(i.Length!=0)
            {
                sentence.GetComponent<Text>().text=index+"    "+i;
                var item=Instantiate(sentence,new Vector3(0,0,0),Quaternion.identity);
                item.transform.SetParent(Content[num].transform);
                index++;
            }   
        }
    }
}
