using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using Newtonsoft.Json;

public class TalkManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    private int index = 0;
    void LoadJsonData_FromPath(string sPath)//경로를 기반으로 Json파일 읽기
    {
        System.IO.FileInfo loadfile = new System.IO.FileInfo(sPath);//
        if(loadfile.Exists==false)
        {
            Debug.Log("파일없음");
            return;
        }
        string sJsonData = File.ReadAllText(loadfile.FullName);
        Load_Character(sJsonData);
    }
    void Load_Character(string sJsonData)
    {

    }
    public void Action(GameObject scanObj)
    {
        if(isAction)//Exit Action
        {
            isAction = false;
        }
        else
        {
            isAction = true;
           
        }
        talkPanel.SetActive(isAction);
    }
}