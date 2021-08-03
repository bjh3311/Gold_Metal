﻿using System.Collections;
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

    public enum CurrentState { idle, walk, warn ,faint,question};
    public CurrentState curState = CurrentState.idle;//시작은 idle상태

    private int walkOridle = 1;//1이면 idle -1이면 walk ,그 이외의 경우에는 0
    private int leftOrright = -1;//-1이면 왼쪽 1이면 오른쪽 소리이다.
    private int layerMask1;

    private bool isWall = false;//true면 벽에 붙어있다는 소리이다.
    private bool isHit = false;//여의주에 맞았는지 아닌지 판별해주는 bool변수
    private bool isdragonBall = false;//여의주가 시야에 들어와있는지 판별해주는 bool변수

    public Transform wallCheck;
    public LayerMask wallLayers;//벽에 붙었는지 아닌지 확인하기 위한 변수들
    private float maxtime = 3.0f;
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
    private void OnTriggerEnter2D(Collider2D col)//여의주에 맞으면 faint상태로 변경
    {
        if(col.gameObject.tag=="dragonBall")
        {
            walkOridle = 0;
            curState = CurrentState.faint;
        }
    }
    private void FixedUpdate()
    {
        layerMask1 = (-1) - (1 << LayerMask.NameToLayer("dragonBall"));//여의주를 제외한 충돌감지 위함
        if(leftOrright==-1)//왼쪽을 바라 보고 있을때는 왼쪽으로 ray를 쏜다
        {
            playerHit = Physics2D.Raycast(transform.position+Vector3.left*1.0f, new Vector3(-1, 0, 0),Mathf.Infinity,layerMask1);
            //시야는 무한으로 여의주제외하고 감지
            dragonballHit = Physics2D.Raycast(transform.position + Vector3.left * 1.0f, new Vector3(-1, 0, 0));
            //시야는 무한으로 
            if(playerHit.collider.CompareTag("Player"))
            {
                p.Detected();
            }
            if (dragonballHit.collider.CompareTag("dragonBall"))//여의주가 시야에 들어오면 question상태
            {
                walkOridle = 0;
                curState = CurrentState.question;
            }
        }
        else//오른쪽을 바라 보고 있을때는 오른쪽으로 ray를 쏜다
        {
            playerHit = Physics2D.Raycast(transform.position + Vector3.right * 1.0f, new Vector3(1, 0, 0), Mathf.Infinity, layerMask1);
            //시야는 무한으로 여의주제외하고 감지
            dragonballHit = Physics2D.Raycast(transform.position + Vector3.right * 1.0f, new Vector3(1, 0, 0));
            //시야는 무한으로 플레이어 제외하고 감지
            if (playerHit.collider.CompareTag("Player"))
            {
                p.Detected();
            }
            if (dragonballHit.collider.CompareTag("dragonBall"))//여의주가 시야에 들어오면 question상태
            {
                walkOridle = 0;
                curState = CurrentState.question;
            }
        }
    }
    IEnumerator CheckState()
    {
        while (true)
        {
            MoveTime();//타이머
            dist = (playerTransform.position.x- transform.position.x);
            isWall = Physics2D.OverlapCircle(wallCheck.position, 1.0f, wallLayers);
            if (isWall)//벽을 마주치면 방향 전환
            {
                leftOrright = leftOrright * -1;
            }
            if (Mathf.Abs(dist) <= stat.view && Input.GetButtonDown("Fire1")&&walkOridle!=0)//x 좌표차가 view안에 있고 마우스 좌클릭하면 curState는 walk
            {
                walkOridle = 0;
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
                        leftOrright = -1;
                        rend.flipX = false;
                    }
                    else//플레이어가 오른쪽에 있으면 오른쪽을 바라본다.
                    {
                        leftOrright = 1;
                        rend.flipX = true;
                    }
                    if(delaytime>maxtime)
                    {
                        walkOridle = 1;//warn상태가 끝나면 idle상태여야 한다
                        delaytime = 0;//idle상태가 된후 타이머를 처음부터 시작
                    }
                    break;
                case CurrentState.faint:
                    Debug.Log("Faint상태!!");
                    if(delaytime>maxtime)//타이머
                    {
                        walkOridle = 1;//faint상태가 끝나면 idle상태여야 한다.
                        delaytime = 0;//idle상태가 된 후 타이머를 처음부터 시작.
                    }
                    break;
                case CurrentState.question:
                    Debug.Log("Question상태!!");
                    if(delaytime>maxtime)//타이머
                    {
                        walkOridle = 1;//question상태가 끝나면 idle상태여야 한다.
                        delaytime = 0;//idle상태가 된 후 타이머를 처음부터 시작.
                    }
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
