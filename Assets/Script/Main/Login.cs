using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Main;
    public GameObject Sign;

    public GameObject Password;
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
