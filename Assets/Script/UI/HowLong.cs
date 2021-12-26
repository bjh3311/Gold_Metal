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

    public bool end=false;//끝나는 변수

    void Start()
    {
        nowWhere=this.gameObject.GetComponent<Image>();
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
                GameManager.instance.MapMove.mapSpeed=13f;
                secondup=true;
                SpeedUp();
            }
            else if(dis>33.0f&&!firstup)//한번만 실행하기 위해 firstup bool변수 사용
            {
                GameManager.instance.MapMove.mapSpeed=11.5f;
                firstup=true;
                SpeedUp();
                
            }
            else if(dis>100.0f&&!end)
            {
                GameManager.instance.box.enabled=false;//알수없는 오류로 계속 타임라인을 이용해서 player를 옮기면
                //bottom의 box collider와 충돌해서 걍 완주하면 player box collider를 꺼준다
                GameManager.instance.MapMove.mapSpeed=0f;
                end=true;
                GameManager.instance.ButtonDisabled();//버튼들 비활성화시키는 함수
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
