using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
   public Button[] Buttons;
   public GameObject PauseScreen;
   private void Awake() {
      Time.timeScale=1;
   }
   public void Stop()//Stop버튼
   {
      Time.timeScale=0;
      PauseScreen.gameObject.SetActive(true);
      for(int i=0;i<Buttons.Length;i++)
      {
         Buttons[i].interactable=false;
      }
   }
   public void Restart()//처음부터 다시하기 버튼
   {
      SceneManager.LoadScene("Game");
   }
   public void Resume()//다시하기 버튼
   {
      Time.timeScale=1;
      PauseScreen.gameObject.SetActive(false);
      for(int i=0;i<Buttons.Length;i++)
      {
         Buttons[i].interactable=true;
      }
   }
   public void Menu()//메뉴로 가는 버튼
   {  
      SceneManager.LoadScene("Main");
   }
}
