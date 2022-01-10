using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Main;
    public GameObject Main_Notif;
    public Text Main_Notif_text;
    public GameObject Sign;

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
    private void Awake()
    {
        LoginUrl="bjh3311.cafe24.com/example.php";
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
        yield return webRequest.SendWebRequest();
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
    }
    IEnumerator SignUpCo()
    {
        WWWForm form=new WWWForm();
        WWW webRequest=new WWW(LoginUrl,form);
        yield return webRequest;
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
