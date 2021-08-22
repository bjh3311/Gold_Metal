using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
public class TalkManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    private int index = -1;
    public TextAsset pAsset;

    public int lanNum;//언어 선택

    public Image playerPortrait;
    public Image npcPortrait;

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Z)&&gameObject.activeSelf==true)//대화창이 켜져 있을 때만
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
            if(pData.characterData[index].id=="0")//id가 0이라면 즉, 플레이어라면,C#에서는 부등호 비교가 빠르다.
            {
                playerPortrait.color = new Color(255, 255, 255);//플레이어는 밝게
                npcPortrait.color = new Color(56, 56, 56);//npc는 어둡게
            }
            else//id가 0이외의 즉 NPC라면
            {
                playerPortrait.color = new Color(56, 56, 56);//플레이어는 어둡게
                npcPortrait.color = new Color(255, 255, 255);//npc는 밝게
            }
            talkText.text = pData.characterData[index].language[lanNum].text;
        }
        else
        {
            talkPanel.SetActive(false);
            return;
        }
        
    }
}