﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Main;
    public GameObject Main_Notif;
    public Text Main_Notif_text;

    public GameObject Sign;
    public GameObject Sign_Notif;
    public Text Sign_Notif_text;

    public GameObject Password;

    [Header("LoginPanel")]
    public InputField ID_Login;
    public InputField Pass_Login;

    [Header("FindPassword")]
    public InputField ID_Find;
    public InputField Email_Find;

    [Header("Sign Up")]
    public InputField ID_Sign;
    public InputField Pass_Sign;
    public InputField PassCheck_Sign;
    public InputField Email_Sign;

    private string LoginUrl;
    private string SignUpUrl;
    private string FindUrl;
    private void Awake()
    {
        LoginUrl="bjh3311.cafe24.com/Login.php";
        SignUpUrl="bjh3311.cafe24.com/SignUp.php";
        FindUrl="bjh3311.cafe24.com/Find.php";
    }
    public void GoToSignUp()
    {
        Main.SetActive(false);
        Sign.SetActive(true);
    }
    public void Back()
    {
        Main.SetActive(true);
        Sign.SetActive(false);
        Password.SetActive(false);
    }
    public void SignUp()
    {
        StartCoroutine("SignUpCo");
    }
    IEnumerator LoginCo()
    {
        WWWForm form=new WWWForm();
        form.AddField("Input_user",ID_Login.text);
        form.AddField("Input_pass",Pass_Login.text);

        UnityWebRequest webRequest=UnityWebRequest.Post(LoginUrl,form);
        yield return webRequest.SendWebRequest();//webRequset가 완료될때까지 기다린다
        //www클래스는 안쓰는걸 권장해서 UnityWebRequest 클래스를 사용한다
        if(webRequest.error==null)
        {
            Debug.Log("통신성공!!!");
        }
        else
        {
            Debug.Log(webRequest.error);
        }
        Main_Notif.SetActive(true);
        Main_Notif_text.text=webRequest.downloadHandler.text;
        if(webRequest.downloadHandler.text=="로그인 성공!!")
        {
            yield return new WaitForSecondsRealtime(2.5f);
            SceneManager.LoadScene("Main");
        }
        else
        {
            yield return new WaitForSecondsRealtime(2.5f);
            Main_Notif.SetActive(false);
        }
    }
    IEnumerator SignUpCo()
    {
        WWWForm form=new WWWForm();
        form.AddField("Sign_user",ID_Sign.text);
        form.AddField("Sign_pass",Pass_Sign.text);
        form.AddField("Sign_check",PassCheck_Sign.text);
        form.AddField("Sign_email",Email_Sign.text);

        UnityWebRequest webRequest=UnityWebRequest.Post(SignUpUrl,form);
        yield return webRequest.SendWebRequest();
        if(webRequest.error==null)
        {
            Debug.Log("통신성공!!");
        }
        else
        {
            Debug.Log(webRequest.error);
        }
        Sign_Notif.SetActive(true);
        Sign_Notif_text.text=webRequest.downloadHandler.text;
        yield return new WaitForSecondsRealtime(2.5f);
        Sign_Notif.SetActive(false);
    }
    public void LogIn()
    {       
        StartCoroutine("LoginCo");
    }
    public void PasswordScene()
    {
        Main.SetActive(false);
        Password.SetActive(true);
    }
    public void Submit()
    {

    }
}
