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
    private RaycastHit2D hit;

    public enum CurrentState { idle, walk, warn ,faint,question};
    public CurrentState curState = CurrentState.idle;

    private int walkOridle = 1;//1이면 idle -1이면 walk
    private int leftOrright = -1;//-1이면 왼쪽 1이면 오른쪽 소리이다.
    private bool isWall = false;//true면 벽에 붙어있다는 소리이다.

    private bool dragonBall = false;

    public Transform wallCheck;
    public LayerMask wallLayers;
    private float maxtime = 3.0f;
    private float delaytime;

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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="bullet")
        {
            dragonBall = true;//여의주와 충돌하면 dragonBall을 true로 만들어주어 faint상태로 변경
        }
    }
    private void FixedUpdate()
    {
        if(leftOrright==-1)
        {
            Debug.DrawRay(transform.position + Vector3.left * 1.0f, new Vector3(-1, 0, 0) * stat.view,new Color(0,1,0));
            hit = Physics2D.Raycast(transform.position+Vector3.left*1.0f, new Vector3(-1, 0, 0) ,stat.view);
            if (hit.collider!=null&&hit.collider.name=="Player")//플레이어가 시야에 들어오면
            {
                p.Detected();
            }
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.right * 1.0f, new Vector3(1, 0, 0) * stat.view,new Color(0,1,0));
            hit = Physics2D.Raycast(transform.position+Vector3.right*1.0f, new Vector3(1, 0, 0) ,stat.view);
            if (hit.collider!=null&&hit.collider.name=="Player")//플레이어가 시야에 들어오면
            {
                p.Detected();
            }
        }
    }
    IEnumerator CheckState()
    {
        while (true)
        {
            MoveTime();
            dist = (playerTransform.position.x- transform.position.x);
            isWall = Physics2D.OverlapCircle(wallCheck.position, 1.0f, wallLayers);
            if (isWall)//벽을 마주치면 방향 전환
            {
                leftOrright = leftOrright * -1;
            }
            if(dragonBall)//여의주를 맞으면 아무 상태에서나 바로 faint상태로 접근 가능해야함.
            {
                curState = CurrentState.faint;
                yield return null;
            }
            if(walkOridle==1)
            {
                curState = CurrentState.idle;
            }
            else if (Mathf.Abs(dist) <= stat.view && Input.GetButtonDown("Fire1"))//x 좌표차가 view안에 있고 마우스 좌클릭하면 curState는 walk
            {
                curState = CurrentState.warn;
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
            if(leftOrright==-1)//왼쪽이면
            {

            }
            switch (curState)
            {
                case CurrentState.idle:
                    animator.SetBool("isMoving", false);
                    if (delaytime > maxtime)
                    {
                        delaytime = 0;
                        walkOridle = walkOridle * -1;
                        leftOrright = stat.random[Random.Range(0, 2)];//idle가 끝나고 walk할때는 랜덤한 방향
                    }
                    break;
                case CurrentState.walk:
                    MoveToWall();
                    animator.SetBool("isMoving", true);
                    if (delaytime > maxtime)
                    {
                        delaytime = 0;
                        walkOridle = walkOridle * -1;
                    }
                    break;
                case CurrentState.warn:
                    Debug.Log("경고상태!!");
                    if(dist<0)//플레이어가 왼쪽에 있으면 왼쪽을 바라본다.
                    {
                        rend.flipX = false;
                    }
                    else//플레이어가 오른쪽에 있으면 오른쪽을 바라본다.
                    {
                        rend.flipX = true;
                    }
                    yield return new WaitForSeconds(1.0f);
                    walkOridle = 1;//warn상태가 끝나면 idle상태여야 한다
                    delaytime = 0;//idle상태가 된후 타이머를 처음부터 시작
                    break;
                case CurrentState.faint:
                    Debug.Log("Faint상태!!");
                    yield return new WaitForSeconds(1.5f);
                    walkOridle = 1;//faint상태가 끝나면 idle상태여야 한다.
                    delaytime = 0;//idle상태가 된 후 타이머를 처음부터 시작.
                    dragonBall = false;
                    break;
                case CurrentState.question:
                    Debug.Log("Question상태!!");
                    yield return new WaitForSeconds(1.5f);//
                    walkOridle = 1;//question상태가 끝나면 idle상태여야 한다.
                    delaytime = 0;
                    break;
            }
            yield return null;
        }
    }
    public void MoveTime()//walk와 idle
    {
        delaytime += Time.deltaTime;
    }
    public void MoveToWall()
    {
        if (leftOrright == -1)//왼쪽을 본다면
        {
            rend.flipX = false;
            transform.Translate(new Vector2(-1, 0) * stat.moveSpeed * Time.deltaTime);
        }
        else//오른쪽을 본다면
        {
            rend.flipX = true;
            transform.Translate(new Vector2(1, 0) * stat.moveSpeed * Time.deltaTime);
        }
    }
}
