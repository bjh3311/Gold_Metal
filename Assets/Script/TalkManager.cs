using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using Newtonsoft.Json;
[System.Serializable]
class CharacterData
{
    public string id;
    public string text;
}
[System.Serializable]
class Data
{
    public CharacterData[] characterData;
}
public class TalkManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    private int index = 0;
    public TextAsset pAsset;
    void LoadJsonData_FromAsset(TextAsset pAsset)//에셋 기반 json불러오기
    {
        if(pAsset==null)
        {
            Debug.LogError("파일 없음");
            return;
        }
        Load_Character(pAsset.text);
    }
    void Load_Character(string sJsonData)
    {
        Data pData = JsonUtility.FromJson<Data>(sJsonData);
        talkText.text = pData.characterData[index].id + "\n" + pData.characterData[index].text;
        if(index+1==pData.characterData.Length)
        {
            talkPanel.SetActive(false);
            return;
        }
        else
        {
            index++;
        }
    }
    public void Action(GameObject scanObj)
    {
        isAction = true;
        LoadJsonData_FromAsset(pAsset);
        talkPanel.SetActive(isAction);
    }
}