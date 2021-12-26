using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
   public GameObject PauseScreen;
   public void Stop()//Stop버튼
   {
      Time.timeScale=0;
      PauseScreen.gameObject.SetActive(true);
      GameManager.instance.ButtonDisabled();
   }
   public void Restart()//처음부터 현재 씬을 다시하는 버튼
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   public void Resume()//다시하기 버튼
   {
      Time.timeScale=1;
      PauseScreen.gameObject.SetActive(false);
      GameManager.instance.ButtonEnabled();
   }
   public void Menu()//메뉴로 가는 버튼
   {  
      SceneManager.LoadScene("Main");
   }
}
