using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class Main : MonoBehaviour
{

    public Button[] MainButton;
    public Image[] MainImage;


    public Button[] StageButton;
    public Image[] StageImage;

    public Text[] Texts;
    public void Play()//Play 버튼
    {
        for(int i=0;i<StageImage.Length;i++)
        {
            StageImage[i].gameObject.SetActive(true);
        }
        ButtonDisabled(MainButton);
        StartCoroutine("ButtonTrans",MainImage);
        StartCoroutine("ButtonUnTrans",StageImage); 
        StartCoroutine("ButtonEnabled",StageButton);
        StartCoroutine("TextUnTrans",Texts);
    }
    public void SelectToMainButton()//Stage 선택창에서 Main으로 돌아가는 버튼
    {
        for(int i=0;i<MainButton.Length;i++)
        {
            MainButton[i].gameObject.SetActive(true);
        }
        ButtonDisabled(StageButton);
        StartCoroutine("ButtonTrans",StageImage);
        StartCoroutine("ButtonUnTrans",MainImage);
        StartCoroutine("ButtonEnabled",MainButton);
        StartCoroutine("TextTrans",Texts);
    }
    public void Exit()//나가기 버튼
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying=false;
        #else
            Application.Quit();
        #endif
    }
    IEnumerator ButtonTrans(Image[] buttons)//버튼들 투명하게
    {
        byte maxTrans=255;
        while(true)
        {
            maxTrans=(byte)(maxTrans-5);
            yield return new WaitForSeconds(0.01f);
            for(int i=0;i<buttons.Length;i++)
            {
                buttons[i].color=new Color32(255,255,255,maxTrans);
            }
            if(maxTrans<=0)
            {
                for(int i=0;i<buttons.Length;i++)
                {
                    buttons[i].gameObject.SetActive(false);
                }
                break;
            }
        }
    }
    IEnumerator ButtonUnTrans(Image[] buttons)//버튼들 드러나게
    {
        byte minTrans=0;
        while(true)
        {
            minTrans=(byte)(minTrans+5);
            yield return new WaitForSeconds(0.01f);
            for(int i=0;i<buttons.Length;i++)
            {
                buttons[i].color=new Color32(255,255,255,minTrans);
            }
            if(minTrans>=255)
            {
                break;
            } 
        }
    }

    IEnumerator TextTrans(Text[] text)
    {
        byte maxTrans=255;
        while(true)
        {
            maxTrans=(byte)(maxTrans-5);
            yield return new WaitForSeconds(0.01f);
            for(int i=0;i<text.Length;i++)
            {
                text[i].color=new Color32((byte)(text[i].color.r*255),(byte)(text[i].color.g*255),(byte)(text[i].color.b*255),maxTrans);
            }
            if(maxTrans<=0)
            {
                break;
            } 
        }

    }
    IEnumerator TextUnTrans(Text[] text)
    {
        byte minTrans=0;
        while(true)
        {
            minTrans=(byte)(minTrans+5);
            yield return new WaitForSeconds(0.01f);
            for(int i=0;i<text.Length;i++)
            {
                text[i].color=new Color32((byte)(text[i].color.r*255),(byte)(text[i].color.g*255),(byte)(text[i].color.b*255),minTrans);
            }
            if(minTrans>=255)
            {
                break;
            } 
        }

    }
    private void ButtonDisabled(Button[] button)//버튼 비활성화, 즉각적으로
    {
        for(int i=0;i<button.Length;i++)
        {
            button[i].interactable=false;
        }
    }
    IEnumerator ButtonEnabled(Button[] button)//버튼 활성화, 조금 텀을 두고
    {
        yield return new WaitForSeconds(1.0f);
        for(int i=0;i<button.Length;i++)
        {
            button[i].interactable=true;
        }
    }
    public void Stage1()//스테이지 1
    {

    }
    public void Stage2()//스테이지 2
    {

    }
    public void Stage3()//스테이지 3
    {

    }
    public void Stage4()//스테이지 4
    {

    }
}
