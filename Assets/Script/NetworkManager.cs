using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private string NetworkUrl;
    private GameObject canvas;
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
        NetworkUrl="bjh3311.cafe24.com/Network.php";
        canvas=this.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        StartCoroutine("Check");
    }
    IEnumerator Check()
    {
        UnityWebRequest webRequest=UnityWebRequest.Get(NetworkUrl);
        yield return webRequest.SendWebRequest();
        if(webRequest.error!=null)//통신에 실패했다
        {
            canvas.SetActive(true);
            yield return new WaitForSecondsRealtime(2.5f);
            Application.Quit();
        }
    }
}   
