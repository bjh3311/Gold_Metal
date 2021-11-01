using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

public class LoadScore : MonoBehaviour
{
    private Text text;
    public GameObject nowScore;
    private void Start() 
    {
        text=this.gameObject.GetComponent<Text>();
    }
    int now;
    private void Update()
    {
        now=nowScore.GetComponent<SaveScore>().score;
        text.text="현재점수: "+now;
    }
}
