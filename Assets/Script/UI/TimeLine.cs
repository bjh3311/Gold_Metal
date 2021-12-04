using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimeLine : MonoBehaviour
{
    public Button[] Buttons;//버튼들
    public GameObject StartScreen;//Start스크린
    public GameObject Ground;//Ground Object
    private MapMove MapMove;//Ground에 붙어있는 MapMove Script
    public GameObject BestScore;
    private SaveScore SaveScore;
    private void Awake()//처음에는 다 꺼져있어야 한다.
    {
        Time.timeScale=1;
        MapMove=Ground.gameObject.GetComponent<MapMove>();
        SaveScore=BestScore.gameObject.GetComponent<SaveScore>();
    }
    public void EndTimeLine()//타임라인 마지막
    {
        for(int i=0;i<Buttons.Length;i++)
        {
          Buttons[i].interactable=true;
        }
        StartScreen.SetActive(false);//Start스크린을 꺼준다
        Time.timeScale=1;
        MapMove.mapSpeed=10f;
        SaveScore.StartCoroutine("plus");//점수 상승 시작
    }
}
