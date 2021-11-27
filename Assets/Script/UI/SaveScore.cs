using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.UI;
[System.Serializable]
class Score
{
    public int value;
}

public class SaveScore : MonoBehaviour
{

    Text bestText;
    Text nowText;//현재 점수 텍스트
    public GameObject nowObject;
    public int score=0;//현재 점수
    private int bestScore;//최고점수
    private void Start() 
    {
        StartCoroutine("plus");//점수 저절로 오르게
        bestText=this.gameObject.GetComponent<Text>();
        bestScore=LoadJsonData_FromAsset();//Json에서 최고점수를 받아온다.
        bestText.text="최고점수: "+bestScore;
        nowText=nowObject.gameObject.GetComponent<Text>();
        nowText.text="현재점수: "+score;
    }
    public void Save()//저장하는 함수, 플레이어가 죽으면 자동으로 실행된다.
    {
        if(score<bestScore)//현재 점수가 최고점수보다 작다면 저장하지 않는다.
        {
            return ;
        }
        Score data=new Score();
        data.value=score;
        string temp=JsonUtility.ToJson(data);//data를 json으로 바꿔줌
        temp=Encrypt(temp,"321");//암호는 321
        if(temp!=null)
        {
            File.WriteAllText(Application.dataPath+"/Json"+"/Score.json",temp);//제이슨 저장
        }
    }
    private static int LoadJsonData_FromAsset()//경로 기반 json불러오기
    {
        string pAsset;
        try
        {
            pAsset=File.ReadAllText(Application.dataPath+"/Json"+"/Score.json");
        }
        catch(FileNotFoundException)//처음실행되었다면 파일이없다.그러므로 0을 반환
        {
            Debug.Log("파일 없음");
            return 0;
        }
        return Load_Integer(pAsset);//catch문이 실행이안되면, 즉 파일이 존재하면 실행된다.
    }
    private static int Load_Integer(string sJsonData)
    {
        sJsonData=Decrypt(sJsonData,"321");//복호화
        Score pData = JsonUtility.FromJson<Score>(sJsonData);
        return pData.value;
    }
    public static string Encrypt(string textToEncrypt, string key)//암호화 함수
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 128;
        rijndaelCipher.BlockSize = 128;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }
    public static string Decrypt(string textToDecrypt, string key)//복호화 함수
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 128;
        rijndaelCipher.BlockSize = 128;
        byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
              len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        return Encoding.UTF8.GetString(plainText);
    }
    IEnumerator plus()
    {
        while(true)
        {   
            yield return new WaitForSecondsRealtime(0.1f);
            if(Time.timeScale==1)//진행중인 상황일 때만 점수를 증가시킨다.
            {
                score++;
            }
        }
    
    }
    private void Update() 
    {
        nowText.text="현재점수: "+score;
    }
}
