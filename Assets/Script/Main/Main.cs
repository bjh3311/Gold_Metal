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
    public void Play()//Play 버튼
    {
        ButtonDisabled(MainButton);
        StartCoroutine("ButtonTrans",MainImage);
        StartCoroutine("ButtonUnTrans",StageImage);  
    }
    public void SelectToMainButton()
    {
        ButtonDisabled(StageButton);
        StartCoroutine("ButtonTrans",StageImage);
        StartCoroutine("ButtonUnTrans",MainImage);
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
                break;
            }
        }
    }
    IEnumerator ButtonUnTrans(Image[] buttons)//버튼들 드러나게
    {
        byte minTrans=255;
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
    private void ButtonDisabled(Button[] button)//버튼 비활성화
    {
        for(int i=0;i<button.Length;i++)
        {
            button[i].interactable=false;
        }
    }
    private void ButtonEnabled(Button[] button)//버튼 활성화
    {
        for(int i=0;i<button.Length;i++)
        {
            button[i].interactable=true;
        }
    }
    
}
