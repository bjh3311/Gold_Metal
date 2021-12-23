﻿using System.Collections;
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
    public GameObject BestScore;
    public SaveScore SaveScore;
    public HowLong HowLong;
    public GameObject NowDis;

    public GameObject player;
    public Rigidbody2D rigid;

    private void Awake()
    {
        instance=this;
        Time.timeScale=1;//ReStart할떄를 위해 계속 Time.timeScale은 1로 해준다
        MapMove=Ground.gameObject.GetComponent<MapMove>();
        SaveScore=BestScore.gameObject.GetComponent<SaveScore>();
        HowLong=NowDis.gameObject.GetComponent<HowLong>();
        rigid=player.gameObject.GetComponent<Rigidbody2D>();
    }
}
