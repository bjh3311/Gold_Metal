using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowLong : MonoBehaviour
{
    // Start is called before the first frame update
    Image nowWhere;
    private float dis=0;

    MapMove mapScript;
    public GameObject Ground;
    void Start()
    {
        nowWhere=this.gameObject.GetComponent<Image>();
        mapScript=Ground.gameObject.GetComponent<MapMove>();
    }
    public void StartPlus()//타임라인에서 이걸 참조할것
    {
        StartCoroutine("plus");
    }
    // Update is called once per frame
    void Update()
    {
        nowWhere.fillAmount=dis/100f;
    }
    IEnumerator plus()
    {
        while(true)
        {
            if(Time.timeScale!=0)
            {
                dis=dis+0.1f;
            }
            if(dis>66.0f)
            {
                mapScript.mapSpeed=14f;
            }
            else if(dis>33.0f)
            {
                mapScript.mapSpeed=12f;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
