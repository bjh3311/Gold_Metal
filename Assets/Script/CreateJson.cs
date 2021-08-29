using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
[System.Serializable]
class Contents//언어별 대화 내용
{
    public string text;
}
[System.Serializable]
class CharacterData
{
    public string id;
    public Contents[] language;//언어별로 대화 내용 담기 위해서
}
[System.Serializable]
class Data
{
    public CharacterData[] characterData;
}
public class CreateJson : MonoBehaviour
{
   private void Start() 
    {   
        Data info=new Data();
        info.characterData=new CharacterData[2];
        string save=JsonUtility.ToJson(info);
        if(save!=null)
        {
            File.WriteAllText(Application.dataPath+"/Text"+"/NpcData.json",save);
        } 
    }
}
