using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    private bool isGround=true;//점프제한을위한 변수
    private int jumpCount=2;//2번까지 점프
    [SerializeField]
    LayerMask groundLayer;//땅 체크
    [SerializeField]
    Transform groundCheck;//땅 체크
    private void Start() 
    {
        rigid=this.gameObject.GetComponent<Rigidbody2D>();
        anim=this.gameObject.GetComponent<Animator>();
    }
    public void Jump()
    {
        if(!isGround||jumpCount<=0)//isGround가 false이면 함수 종료
        {
            return;
        }
        rigid.AddForce(new Vector2(0,4.0f),ForceMode2D.Impulse);//점프
        Debug.Log("점프 버튼 눌름");
        jumpCount--;
    }
    public void Attack()
    {
        Debug.Log("Attack");
    }
    public void Crouch()
    {
        Debug.Log("Crouch");
    }
    void FixedUpdate()
    {
        Debug.Log(isGround);
        isGround=Physics2D.OverlapCircle(groundCheck.position,0.5f,groundLayer);
        if(isGround)
        {
            jumpCount=2;
        }
    }
}
