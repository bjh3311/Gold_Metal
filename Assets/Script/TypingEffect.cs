using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public Text DialogText;
    string originText;
    string subText;

    private void Start()
    {
        originText = gameObject.GetComponent<Text>().ToString();
        StartCoroutine("TypingAction");
    }
    IEnumerator TypingAction()
    {
        for (int i = 0; i < originText.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            subText += originText.Substring(0, i);
            DialogText.text = subText;
            subText = "";
        }
    }


}
