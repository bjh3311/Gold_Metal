using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
                playerPortrait.color = new Color32(255, 255, 255,255);//플레이어는 밝게
                npcPortrait.color = new Color32(56, 56, 56,255);//npc는 어둡게
            }
            else//id가 0이 아니라면 즉 NPC라면
            {
                playerPortrait.color = new Color32(56, 56, 56,255);//플레이어는 어둡게
                npcPortrait.color = new Color32(255, 255, 255,255);//npc는 밝게
            }//Color32의 마지막 인자는 투명도를 나타냄. 0이면 완전투명이어서 안보이고 255면 완전 불투명
            talkText.text = pData.characterData[index].language[lanNum].text;
        }
        else
        {
            talkPanel.SetActive(false);
            return;
        }
    }
}