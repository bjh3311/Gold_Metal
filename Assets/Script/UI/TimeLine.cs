﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimeLine : MonoBehaviour
{
    public void EndTimeLine()//타임라인 마지막
    {
        for(int i=0;i<GameManager.instance.Buttons.Length;i++)
        {
          GameManager.instance.Buttons[i].interactable=true;
          Debug.Log("df");
        }
        GameManager.instance.StartScreen.SetActive(false);//Start스크린을 꺼준다
        Time.timeScale=1;
        GameManager.instance.MapMove.mapSpeed=10f;
        GameManager.instance.SaveScore.StartCoroutine("plus");//점수 상승 시작
        GameManager.instance.HowLong.StartPlus();// 얼마나 왔는지 확인해주는 게이지 상승 시작
    }
}
