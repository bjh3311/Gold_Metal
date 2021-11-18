using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    
    public GameObject GameOverScreen;//게임오버화면
    public Button[] Buttons;//버튼들
    Vector3 cameraPos;//초기 카메라 위치

    [SerializeField] [Range(0.01f,0.1f)] float shakeRange=0.05f;
    [SerializeField] float duration=0.5f;

    public void Shake()
    {
        
        #if UNITY_ANDROID
        Handheld.Vibrate();//휴대폰 진동
        #endif
        cameraPos=mainCamera.transform.position;//초기 카메라 위치를 저장해준다
        InvokeRepeating("StartShake",0f,0.005f);
        Invoke("StopShake",duration);
    }
    void StartShake()
    {
        float cameraPosX=Random.value*shakeRange*2-shakeRange;
        float cameraPosy=Random.value*shakeRange*2-shakeRange;
        Vector3 temp=mainCamera.transform.position;
        temp.x+=cameraPosX;
        temp.y+=cameraPosy;
        mainCamera.transform.position=temp;
    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position=cameraPos;//카메라를 다시 초기위치로 이동시켜준다.
        Invoke("GameOver",0.3f);//카메라 흔들기가 끝난 후 게임오버 시킨다.
    }
    void GameOver()
    {
        Time.timeScale=0;//정지 시킨다.
        GameOverScreen.SetActive(true);
        for(int i=0;i<Buttons.Length;i++)
        {
            Buttons[i].interactable=false;
        }
    }
}
