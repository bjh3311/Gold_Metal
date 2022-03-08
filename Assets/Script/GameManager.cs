using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;

    public Button[] Buttons;//버튼들
    public GameObject StartScreen;//Start스크린
    public GameObject Ground;//Ground Object
    public MapMove MapMove;//Ground에 붙어있는 MapMove Script
    public HowLong HowLong;
    public GameObject NowDis;
    public GameObject player;

    public BoxCollider2D box;
    public AudioSource Audio;
    public int Stage;//user가 몇 스테이지 까지 깻는지를 나타낸다. 2이면 1은 이미 깨고 2는 아직.
    public string ID;
    private string NowStageUrl;

    private string DeathUrl;
    public Text DeathText;

    private void Awake()
    {
        instance=this;
        Time.timeScale=1;//ReStart할떄를 위해 계속 Time.timeScale은 1로 해준다
        MapMove=Ground.gameObject.GetComponent<MapMove>();  
        HowLong=NowDis.gameObject.GetComponent<HowLong>();
        box=player.gameObject.GetComponent<BoxCollider2D>();
        Audio=this.gameObject.GetComponent<AudioSource>();
        NowStageUrl="bjh3311.cafe24.com/NowStage.php";
        DeathUrl="bjh3311.cafe24.com/Death.php";
        LoadJsonData_FromAsset_ID();
        StartCoroutine("LoadStage_FromDB");
    }
    public void ButtonDisabled()//버튼 비활성화 시키는 함수
    {
        for(int i=0;i<Buttons.Length;i++)
        {
            Buttons[i].interactable=false;
        }
    }
    public void ButtonEnabled()//버튼 활성화 시키는 함수
    {
        for(int i=0;i<Buttons.Length;i++)
        {
            Buttons[i].interactable=true;
        }
    }
    private void LoadJsonData_FromAsset_ID()//경로 기반 json 불러오기
    {
        string pAsset;
        pAsset=File.ReadAllText(Application.persistentDataPath+"/User.json");
        User temp=JsonUtility.FromJson<User>(pAsset);
        ID=temp.ID;//ID불러오기
        StartCoroutine("Load_Death");//ID불러오는게 끝나고 죽은횟수 불러오기
    }
    private IEnumerator LoadStage_FromDB()//DB에서  Stage값 불러오기
    {
        WWWForm form=new WWWForm();
        form.AddField("Find_user",ID);
        UnityWebRequest webRequest=UnityWebRequest.Post(NowStageUrl,form);
        yield return webRequest.SendWebRequest();
        string temp=webRequest.downloadHandler.text;
        Stage=int.Parse(temp);
    }
     private IEnumerator Load_Death()//현재 몇번 죽었는지 불러오는 코루틴
    {
        WWWForm form=new WWWForm();
        form.AddField("Input_ID",ID);
        form.AddField("Input_Stage",SceneManager.GetActiveScene().buildIndex-1);//현재 몇 단계인지
        UnityWebRequest webRequest=UnityWebRequest.Post(DeathUrl,form);
        yield return webRequest.SendWebRequest();//webRequset가 완료될때까지 기다린다
        //www클래스는 안쓰는걸 권장해서 UnityWebRequest 클래스를 사용한다
        if(webRequest.error==null)
        {
            Debug.Log("죽음 불러옴!!");
        }
        else
        {
            Debug.Log(webRequest.error);
        }
        DeathText.text="Death : "+webRequest.downloadHandler.text;
    }
}
