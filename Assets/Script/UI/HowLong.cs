using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

public class HowLong : MonoBehaviour
{

    public PlayableDirector pDirector;
    Image nowWhere;
    private float dis=0;

    public GameObject Speed;//SpeedUp 화면
    private bool firstup=false;//첫번째 속도 up
    private bool secondup=false;//두번째 속도 up

    public bool end=false;//끝나는 변수

    private string NewStageUrl;
    
    public GameObject NewStageOpen;
    private bool isOpen;

    void Awake()
    {
        nowWhere=this.gameObject.GetComponent<Image>();
        NewStageUrl="bjh3311.cafe24.com/NewStage.php";
        isOpen=false;
    }
    // Update is called once per frame
    void Update()
    {
        nowWhere.fillAmount=dis/100f;
    }
    IEnumerator plus()
    {
        while(true)
        {
            if(Time.timeScale!=0&&!end)
            {
                dis=dis+0.19f;
            }
            if(dis>66.0f&&!secondup)//한번만 실행하기 위해 secondup bool변수 사용
            {
                GameManager.instance.MapMove.mapSpeed=13f;
                secondup=true;
                SpeedUp();
            }
            else if(dis>33.0f&&!firstup)//한번만 실행하기 위해 firstup bool변수 사용
            {
                GameManager.instance.MapMove.mapSpeed=11.5f;
                firstup=true;
                SpeedUp();
            }
            else if(dis>100.0f&&!end)
            {
                GameManager.instance.box.enabled=false;//알수없는 오류로 계속 타임라인을 이용해서 player를 옮기면
                //bottom의 box collider와 충돌해서 걍 완주하면 player box collider를 꺼준다
                GameManager.instance.MapMove.mapSpeed=0f;
                end=true;
                GameManager.instance.ButtonDisabled();//버튼들 비활성화시키는 함수
                pDirector.Play();
                StartCoroutine("BgmOff");
                if(SceneManager.GetActiveScene().buildIndex-1==GameManager.instance.Stage)
                {
                    StartCoroutine("NewStageCo");
                    isOpen=true;
                }//최대 Stage를 깻다면 새로운 Stage를 열어준다
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    IEnumerator BgmOff()
    {
        while(GameManager.instance.Audio.volume>0)
        {
            GameManager.instance.Audio.volume-=Time.deltaTime*0.2f;
            yield return null;
        }    
    }
    private void SpeedUp()//Speed Up 할 때 나타나는 효과
    {
        Speed.SetActive(true);
        Invoke("SpeedUpOff",2.0f);
    }
    private void SpeedUpOff()
    {
        Speed.SetActive(false);
    }
    IEnumerator NewStageCo()
    {
        WWWForm form=new WWWForm();
        form.AddField("Input_ID",GameManager.instance.ID);
        form.AddField("Input_Stage",(GameManager.instance.Stage)+1);
        UnityWebRequest webRequest=UnityWebRequest.Post(NewStageUrl,form);
        yield return webRequest.SendWebRequest();//webRequset가 완료될때까지 기다린다
        //www클래스는 안쓰는걸 권장해서 UnityWebRequest 클래스를 사용한다
        if(webRequest.error==null)
        {
            Debug.Log("새로운 스테이지 활성화 완료!!!");
        }
        else
        {
            Debug.Log(webRequest.error);
        }
    }
    public void OpenStage()//새로운 스테이지를 연다는 메세지를 띄운다
    {
        if(SceneManager.GetActiveScene().buildIndex-1==4)//마지막 스테이지일시 newstage Open은 안띄운다
        {
            return;
        }
        else if(isOpen)
        {
            NewStageOpen.SetActive(true);
        }
    }
}
