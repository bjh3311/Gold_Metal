using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class Main : MonoBehaviour
{
    public void Play()//Play 버튼
    {
        StartCoroutine("MainToSelect");
    }
    public void SelectToMainButton()
    {
        StartCoroutine("SelectToMain");
    }
    public void Exit()//나가기 버튼
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying=false;
        #else
            Application.Quit();
        #endif
    }
    IEnumerator MainToSelect()//Main에서 Select로
    {
        yield return null;
    }
    IEnumerator SelectToMain()//Select에서 Main으로
    {
        yield return null;
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
