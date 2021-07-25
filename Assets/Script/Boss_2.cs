using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Boss_2 : MonoBehaviour
{
    public MonsterStatus stat;
    private Animator animator;
    private SpriteRenderer rend;
    private Transform transform;
    private Transform playerTransform;
    public enum CurrentState { idle, walk, warn };
    public CurrentState curState = CurrentState.idle;

    private int walkOridle = 1;//1이면 idle -1이면 walk

    private float maxtime = 3.0f;
    private float delaytime;
    // Start is called before the first frame update
    void Start()
    {
        transform = this.gameObject.GetComponent<Transform>();
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }
    IEnumerator CheckState()
    {
        while (true)
        {
            MoveTime();
            float dist = Vector2.Distance(playerTransform.position, transform.position);
            if (dist <= stat.view && Input.GetButtonDown("Fire1"))//거리가 view안에 있고 마우스 좌클릭하면 curState는 walk
            {
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
                    }
                    break;
                case CurrentState.walk:
                    if (playerTransform.position.x - transform.position.x < 0)//타겟이 왼쪽에 있다면
                    {
                        rend.flipX = true;
                    }
                    else//타겟이 오른쪽에 있다면
                    {
                        rend.flipX = false;
                    }
                    MoveToTarget();
                    animator.SetBool("isMoving", true);
                    if (delaytime > maxtime)
                    {
                        delaytime = 0;
                        walkOridle = walkOridle * -1;
                    }
                    break;
                case CurrentState.warn:
                    Debug.Log("경고상태_1!!");
                    yield return new WaitForSeconds(1.0f);
                    break;

            }
            yield return null;
        }
    }
    public void MoveTime()
    {
        delaytime += Time.deltaTime;
    }
    public void MoveToTarget()
    {
        float dir = playerTransform.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * stat.moveSpeed * Time.deltaTime);
    }
}
