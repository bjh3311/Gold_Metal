using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Boss_1 : MonoBehaviour
{
    public MonsterStatus stat;
    private Animator animator;
    private SpriteRenderer rend;
    private Transform transform;
    private Transform playerTransform;
    public GameObject player;
    private Player p;

    private RaycastHit2D playerHit;//player감지를 위한 변수
    private RaycastHit2D dragonballHit;//여의주감지를 위한 변수
    private RaycastHit2D groundHit;//낭떠러지인지 확인을 위한 변수

    public enum CurrentState { idle, walk, warn ,faint,question};
    public CurrentState curState = CurrentState.idle;//시작은 idle상태

    private int walkOridle = 1;//1이면 idle -1이면 walk ,그 이외의 경우에는 0
    private int layerMask1;
    private int layerGround;//낭떠러지인지 확인을 위한 layer변수

    private bool isWall = false;//true면 벽에 붙어있다는 소리이다.

    public Transform wallCheck;
    public LayerMask wallLayers;//벽에 붙었는지 아닌지 확인하기 위한 변수들
    private float delaytime;//walk와 idle간 상태 변화를 위한 타이머

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        transform = this.gameObject.GetComponent<Transform>();
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        p = player.GetComponent<Player>();
        
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }
    private void OnCollisionEnter2D(Collision2D col)//여의주에 맞으면 faint상태로 변경
    {
        if(col.gameObject.tag=="dragonBall"&&curState!=CurrentState.faint)//faint상태가 아닐때만
        {
            walkOridle = 0;
            curState = CurrentState.faint;
        }
    }
    private void FixedUpdate()
    {
        layerMask1 = (-1) - (1 << LayerMask.NameToLayer("Yeouiju"));//여의주를 제외한 충돌감지 위함
        layerGround = 1 << LayerMask.NameToLayer("Ground");//Ground Layer만 체크한다.
        Vector2 frontVec;
        if(!rend.flipX)//왼쪽을 바라 보고 있을때는 왼쪽으로 ray를 쏜다
        {
            playerHit = Physics2D.Raycast(transform.position+Vector3.left*1.0f, new Vector3(-1, 0, 0),Mathf.Infinity,layerMask1);
            //시야는 무한으로 여의주제외하고 감지
            dragonballHit = Physics2D.Raycast(transform.position + Vector3.left * 1.0f, new Vector3(-1, 0, 0));
            //시야는 무한으로 
            frontVec = new Vector2(transform.position.x -stat.groundView, transform.position.y);
            groundHit = Physics2D.Raycast(frontVec, Vector3.down, stat.groundDepth, layerGround);
            Debug.DrawRay(frontVec, Vector3.down*stat.groundDepth, new Color(0, 1, 0));
            if(playerHit.collider!=null&&playerHit.collider.CompareTag("Player")&&curState!=CurrentState.faint)//faint상태가 아닐때만
            {
                p.Detected();
            }
            if (dragonballHit.collider!=null&&dragonballHit.collider.CompareTag("dragonBall"))//여의주가 시야에 들어오면 question상태
            {
                walkOridle = 0;
                curState = CurrentState.question;
            }
            if(groundHit.collider==null)//절벽 체크
            {
                rend.flipX = !rend.flipX;
            }
        }
        else//오른쪽을 바라 보고 있을때는 오른쪽으로 ray를 쏜다
        {
            playerHit = Physics2D.Raycast(transform.position + Vector3.right * 1.0f, new Vector3(1, 0, 0), Mathf.Infinity, layerMask1);
            //시야는 무한으로 여의주제외하고 감지
            dragonballHit = Physics2D.Raycast(transform.position + Vector3.right * 1.0f, new Vector3(1, 0, 0));
            //시야는 무한으로 
            frontVec = new Vector2(transform.position.x + stat.groundView, transform.position.y);
            groundHit = Physics2D.Raycast(frontVec, Vector3.down, stat.groundDepth, layerGround);
            Debug.DrawRay(frontVec, Vector3.down*stat.groundDepth, new Color(0, 1, 0));
            if (playerHit.collider!=null&&playerHit.collider.CompareTag("Player")&&curState!=CurrentState.faint)//faint상태가 아닐때만
            {
                p.Detected();
            }
            if (dragonballHit.collider!=null&&dragonballHit.collider.CompareTag("dragonBall"))//여의주가 시야에 들어오면 question상태
            {
                walkOridle = 0;
                curState = CurrentState.question;
            }
            if(groundHit.collider==null)//절벽 체크
            {
                rend.flipX = !rend.flipX;
            }
        }
    }
    IEnumerator CheckState()
    {
        while (true)
        {
            MoveTime();//타이머
            dist = (playerTransform.position- transform.position).sqrMagnitude;//반원식으로 
            isWall = Physics2D.OverlapCircle(wallCheck.position, 0.5f, wallLayers);//벽과의 거리
            if (isWall)//벽을 마주치면 방향 전환
            {
                rend.flipX = !rend.flipX;
            }
            if (dist <= stat.view*stat.view && Input.GetButtonDown("Fire1")&&walkOridle!=0)//x 좌표차가 view안에 있고 마우스 좌클릭하면 curState는 walk
            {
                walkOridle = 0;
                delaytime = 0;
                curState = CurrentState.warn;
            }
            else if (walkOridle == 1)
            {
                curState = CurrentState.idle;
            }
            else if (walkOridle == -1)
            {
                curState = CurrentState.walk;
            }
            yield return null;
        }
    }
    IEnumerator CheckStateForAction()
    {
        while (true)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    animator.SetBool("isMoving", false);
                    if (delaytime > stat.maxTime)
                    {
                        delaytime = 0;//타이머 초기화
                        walkOridle = walkOridle * -1;
                        rend.flipX = stat.random[Random.Range(0, 2)];//idle가 끝나고 walk할때는 랜덤한 방향
                    }
                    break;
                case CurrentState.walk:
                    MoveToWall();
                    animator.SetBool("isMoving", true);
                    Init();
                    break;
                case CurrentState.warn:
                    Debug.Log("경고상태!!");
                    if(playerTransform.position.x-transform.position.x<0)//플레이어가 왼쪽에 있으면 왼쪽을 바라본다.
                    {
                        rend.flipX = false;
                    }
                    else//플레이어가 오른쪽에 있으면 오른쪽을 바라본다.
                    {
                        rend.flipX = true;
                    }
                    Init();
                    break;
                case CurrentState.faint:
                    Debug.Log("Faint상태!!");
                    yield return new WaitForSeconds(stat.maxTime);
                    walkOridle = 1;//faint상태가 끝나면 idle상태여야 한다.
                    delaytime = 0;//idle상태가 된 후 타이머를 처음부터 시작.  
                    break;
                case CurrentState.question:
                    Debug.Log("Question상태!!");
                    Init();
                    break;
            }
            yield return null;
        }
    }
    private void Init()//각 상태가 타이머에 의해서 만료될 때 취할 행동
    {
        if(delaytime>stat.maxTime&&(walkOridle==-1||walkOridle==1))//idle나 walk일때 타이머가 만료되면
        {
            walkOridle = walkOridle*-1;
            delaytime = 0;
        }
        else if(delaytime>stat.maxTime)//faint를 제외한 나머지 경우에서 타이머가 만료되면
        {
            walkOridle = 1;
            delaytime = 0;
        }
        
    }
    public void MoveTime()//walk와 idle
    {
        delaytime += Time.deltaTime;
    }
    public void MoveToWall()
    {
        if (!rend.flipX)//왼쪽을 본다면
        {
            transform.Translate(new Vector2(-1, 0) * stat.moveSpeed * Time.deltaTime);
        }
        else//오른쪽을 본다면
        {
            transform.Translate(new Vector2(1, 0) * stat.moveSpeed * Time.deltaTime);
        }
    }
}
