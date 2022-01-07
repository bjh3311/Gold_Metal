using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Main;
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

    public string LoginUrl;
    private void Awake()
    {
        LoginUrl="bjh3311.cafe24.com/dbeditor/Login.php";
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

    }
    IEnumerator LoginCo()
    {
        Debug.Log(ID_Login.text);
        Debug.Log(Pass_Login.text);
        WWWForm form=new WWWForm();
        form.AddField("Input_user",ID_Login.text);
        form.AddField("Input_pass",Pass_Login.text);

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
