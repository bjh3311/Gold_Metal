using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;
public class LoadScore : MonoBehaviour
{
    Text contents;
    public GameObject nowScore;
    private SaveScore script; 
    private void Start() 
    {
        contents=this.gameObject.GetComponent<Text>();
    }
    public int now=0;
    private void Update()
    {
        contents.text="현재점수: "+now;
    }
}
