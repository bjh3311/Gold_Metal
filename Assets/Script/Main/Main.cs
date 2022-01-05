﻿using System.Collections;
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

    public GameObject Black;

    public Text Loading;
    
    public Image Bar;
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
    IEnumerator TypingEffect(string dot)//Loading.... 에서 . 을 타이핑되는듯이 계속
    {   
        int i=0;
        while(true)
        {
            for(i=0;i<dot.Length;i++)
            {
                Loading.text="Loading"+dot.Substring(0,i+1);
                yield return new WaitForSeconds(0.5f);
            }
            Loading.text="Loading";
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void LoadScene(string sceneName)
    {
        ButtonDisabled(StageButton);
        Black.gameObject.SetActive(true);
        StartCoroutine("TypingEffect","......");
        StartCoroutine("Load",sceneName);
    }
    private IEnumerator Load(string sceneName)
    {
        AsyncOperation op=SceneManager.LoadSceneAsync(sceneName);
        //비동기 방식으로 씬을 불러오는 도중에도 다른 작업을 할 수  LoadSceneAsync 함수
        //로딩의 진행정도는 AsyncOperation Class로 반환된다
        op.allowSceneActivation=false;//로딩이 끝나면 씬을 바로 시작 못하게 한다
        float timer=0.0f;
        while(!op.isDone)//isDone이 false일 때 동안, 즉 Load가 진행중을 의미한다
        {
            yield return null;
            timer+=Time.unscaledDeltaTime;
            Bar.fillAmount=Mathf.Lerp(0,1f,timer);
            if(Bar.fillAmount==1.0f)//fillAmount가 다 차면
            {
                yield return new WaitForSeconds(4.0f);
                op.allowSceneActivation=true;
                break;
            }
        }    
    }
}
