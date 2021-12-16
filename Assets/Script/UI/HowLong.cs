using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowLong : MonoBehaviour
{
    // Start is called before the first frame update
    Image nowWhere;
    private float dis=0;


    private bool firstup=false;//첫번째 속도 up
    private bool secondup=false;//두번째 속도 up
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
                dis=dis+0.19f;
            }
            if(dis>66.0f&&!secondup)
            {
                mapScript.mapSpeed=13f;
                secondup=true;
                SpeedUp();
            }
            else if(dis>33.0f&&!firstup)
            {
                mapScript.mapSpeed=11.5f;
                firstup=true;
                SpeedUp();
            }
            Debug.Log(dis);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    private void SpeedUp()//Speed Up 할 때 나타나는 효과
    {

    }
}
