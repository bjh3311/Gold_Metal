using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using Newtonsoft.Json;

class Data
{
    public string id;
    public string text;
}
public class TalkManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    private int index = 0;
    public void Action(GameObject scanObj)
    {
        Data data = new Data();
        if(isAction)//Exit Action
        {
            isAction = false;
        }
        else
        {
            string JsonString = File.ReadAllText(Application.dataPath + "/Text/npc정보.json");
            data = JsonConvert.DeserializeObject<Data>(JsonString);
            Debug.Log(data.id);
            Debug.Log(data.id);
            isAction = true;
            index++;
        }
        talkPanel.SetActive(isAction);
    }
}