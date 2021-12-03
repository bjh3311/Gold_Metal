using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimeLine : MonoBehaviour
{
    public Button[] Buttons;//버튼들
    public GameObject StartScreen;//Start스크린
    private void Awake()//처음에는 다 꺼져있어야 한다.
    {
        Time.timeScale=1;
    }
    public void EndTimeLine()//타임라인 마지막
    {
        for(int i=0;i<Buttons.Length;i++)
        {
          Buttons[i].interactable=true;
        }
        StartScreen.SetActive(false);//Start스크린을 꺼준다
        Time.timeScale=1;
        Debug.Log("EndTimeLine");
    }
}
