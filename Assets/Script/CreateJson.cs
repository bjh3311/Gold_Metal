using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
        info.characterData[0].id="배진환";
        info.characterData[0].language=new language[3];
        info.characterData[0].language[0]="한국어";
        info.characterData[0].langauge[1]="영어";
        info.characterData[0].langauge[2]="일본어";
        info.characterData[1].id="장한준";
        info.characterData[1].language=new langague[3];
        info.characterData[1].language[0]="한국어";
        info.characterData[1].langauge[1]="영어";
        info.characterData[1].langauge[2]="일본어";
        string save=JsonUtility.ToJson(info);
        if(save!=null)
        {
            File.WriteAllText(Application.dataPath+"/Text"+"/NpcData.json",save);
        } 
    }
}
