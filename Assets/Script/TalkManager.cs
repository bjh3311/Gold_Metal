using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
    private int index = -1;
    public TextAsset pAsset;

    public Image playerPortrait;
    public Image npcPortrait;

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            index++;
            LoadJsonData_FromAsset(pAsset);
        }
    }
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
        if (index < pData.characterData.Length)
        {
            talkText.text = pData.characterData[index].id + "\n" + pData.characterData[index].text;
        }
        else
        {
            talkPanel.SetActive(false);
            return;
        }
        
    }
}