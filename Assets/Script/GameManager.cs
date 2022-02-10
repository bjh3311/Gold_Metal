using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Awake()
    {
        instance=this;
        Time.timeScale=1;//ReStart할떄를 위해 계속 Time.timeScale은 1로 해준다
        MapMove=Ground.gameObject.GetComponent<MapMove>();  
        HowLong=NowDis.gameObject.GetComponent<HowLong>();
        box=player.gameObject.GetComponent<BoxCollider2D>();
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
}
