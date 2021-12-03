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
    private MapMove script;//Ground에 붙어있는 MapMove Script
    private void Awake()//처음에는 다 꺼져있어야 한다.
    {
        Time.timeScale=1;
        script=Ground.gameObject.GetComponent<MapMove>();
    }
    public void EndTimeLine()//타임라인 마지막
    {
        for(int i=0;i<Buttons.Length;i++)
        {
          Buttons[i].interactable=true;
        }
        StartScreen.SetActive(false);//Start스크린을 꺼준다
        Time.timeScale=1;
        script.mapSpeed=10f;
        
    }
}
