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
    public void LogIn()
    {       

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
