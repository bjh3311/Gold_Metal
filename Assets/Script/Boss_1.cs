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
    public enum CurrentState { idle, walk };
    public CurrentState curState = CurrentState.idle;
    // Start is called before the first frame update
    void Start()
    {
        transform = this.gameObject.GetComponent<Transform>();
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();

        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }
    IEnumerator CheckState()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector2.Distance(stat.playerTransform.position, transform.position);
            if (dist <= stat.view)//거리가 view안에 있다면 curState는 walk
            {
                curState = CurrentState.walk;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
        

    }
    IEnumerator CheckStateForAction()
    {
        while(true)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    animator.SetBool("isMoving", false);
                    break;
                case CurrentState.walk:
                    if (stat.playerTransform.position.x - transform.position.x < 0)//타겟이 왼쪽에 있다면
                    {
                        rend.flipX = false;
                    }
                    else//타겟이 오른쪽에 있다면
                    {
                        rend.flipX = true;
                    }
                    MoveToTarget();
                    animator.SetBool("isMoving", true);
                    break;

            }
            yield return null;
        }
    }
    public void MoveToTarget()
    {
        float dir = stat.playerTransform.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * stat.moveSpeed * Time.deltaTime);
    }
}
