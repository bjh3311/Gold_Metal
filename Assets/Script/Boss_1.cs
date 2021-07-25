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
    public enum CurrentState { idle, walk, warn };
    public CurrentState curState = CurrentState.idle;

    private int walkOridle = 1;//1이면 idle -1이면 walk
    private int leftOrright = -1;//-1이면 왼쪽 1이면 오른쪽 소리이다.
    private bool isWall = false;//true면 벽에 붙어있다는 소리이다.

    public Transform wallCheck;
    public LayerMask wallLayers;
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
            isWall = Physics2D.OverlapCircle(wallCheck.position, 0.5f, wallLayers);
            if (isWall)
            {
                leftOrright = leftOrright * -1;
            }
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
                    MoveToWall();
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
