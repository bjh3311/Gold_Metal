using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class HowLong : MonoBehaviour
{

    public PlayableDirector pDirector;
    Image nowWhere;
    private float dis=0;

    public GameObject Speed;//SpeedUp 화면
    private bool firstup=false;//첫번째 속도 up
    private bool secondup=false;//두번째 속도 up

    private bool end=false;//끝나는 변수
    MapMove mapScript;
    public GameObject Ground;
    void Start()
    {
        nowWhere=this.gameObject.GetComponent<Image>();
        mapScript=Ground.gameObject.GetComponent<MapMove>();
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
            if(Time.timeScale!=0&&!end)
            {
                dis=dis+0.19f;
            }
            if(dis>66.0f&&!secondup)//한번만 실행하기 위해 secondup bool변수 사용
            {
                mapScript.mapSpeed=13f;
                secondup=true;
                SpeedUp();
            }
            else if(dis>33.0f&&!firstup)//한번만 실행하기 위해 firstup bool변수 사용
            {
                mapScript.mapSpeed=11.5f;
                firstup=true;
                SpeedUp();
            }
            else if(dis>35.0f&&!end)
            {
                mapScript.mapSpeed=0f;
                end=true;
                for(int i=0;i<GameManager.instance.Buttons.Length;i++)
                {
                    GameManager.instance.Buttons[i].interactable=false;
                }
                GameManager.instance.rigid.velocity=new Vector2(13.0f,0);
                pDirector.Play();
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    private void SpeedUp()//Speed Up 할 때 나타나는 효과
    {
        Speed.SetActive(true);
        Invoke("SpeedUpOff",2.0f);
    }
    private void SpeedUpOff()
    {
        Speed.SetActive(false);
    }
}
