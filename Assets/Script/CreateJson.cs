using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;
[System.Serializable]
class Contents//언어별 대화 내용
{
    public string text;
}
[System.Serializable]
class CharacterData
{
    public string id;
    public Contents[] language;//언어별로 대화 내용 담기 위해서
}
[System.Serializable]
class Data
{
    public CharacterData[] characterData;
}
public class CreateJson : MonoBehaviour
{
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
   private void Start() 
    {   
        Data info=new Data();
        info.characterData=new CharacterData[2];
        info.characterData[0]=new CharacterData();
        info.characterData[0].id="0";
        info.characterData[0].language=new Contents[3];
        info.characterData[0].language[0]=new Contents();
        info.characterData[0].language[0].text="배진환 한국어";
        info.characterData[0].language[1]=new Contents();
        info.characterData[0].language[1].text="배진환 영어";
        info.characterData[0].language[2]=new Contents();
        info.characterData[0].language[2].text="배진환 일본어";
        info.characterData[1]=new CharacterData();
        info.characterData[1].id="1";
        info.characterData[1].language=new Contents[3];
        info.characterData[1].language[0]=new Contents();
        info.characterData[1].language[0].text="장한준 한국어";
        info.characterData[1].language[1]=new Contents();
        info.characterData[1].language[1].text="장한준 영어";
        info.characterData[1].language[2]=new Contents();
        info.characterData[1].language[2].text="장한준 일본어";
        string save=JsonUtility.ToJson(info);
        save=Encrypt(save,"321");
        if(save!=null)
        {
            File.WriteAllText(Application.dataPath+"/Text"+"/NpcData.json",save);
        } 
    }
}
