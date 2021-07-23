using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//에디터에서 쉽게 사용할수 있도록 메뉴를 만듬
[CreateAssetMenu(fileName ="MonsterStatus",menuName="Scriptable Object Asset/MonsterStatus")]
public class NewBehaviourScript : ScriptableObject
{
    public int moveSpeed = 10;
    public int view = 20;//시야범위

    private Animator animator;
    public SpriteRenderer rend;

    public Transform transform;
    public Transform playerTransform;
    public enum CurrentState { idle,walk};

    public CurrentState curState = CurrentState.idle;
    IEnumerator CheckState()
    {
        
            yield return new WaitForSeconds(0.2f);
            float dist = Vector2.Distance(playerTransform.position, transform.position);
            if (dist <= view)//거리가 view안에 있다면 curState는 walk
            {
                curState = CurrentState.walk;
            }
            else
            {
                curState = CurrentState.idle;
            }
        
    }
    IEnumerator CheckStateForAction()
    {
        switch(curState)
        {
            case CurrentState.idle:
                animator.SetBool("isMoving", false);
                break;
            case CurrentState.walk:
                if(playerTransform.position.x-transform.position.x<0)//타겟이 왼쪽에 있다면
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
    public void MoveToTarget()
    {
        float dir = playerTransform.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
    }
}
