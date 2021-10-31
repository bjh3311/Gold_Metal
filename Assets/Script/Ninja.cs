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
    private LayerMask groundLayer;//땅 체크
    private Transform groundCheck;//땅 체크
    private void Start() 
    {
        rigid=this.gameObject.GetComponent<Rigidbody2D>();
        anim=this.gameObject.GetComponent<Animator>();
        groundCheck=this.gameObject.GetComponent<Transform>();
    }
    public void Jump()
    {
        if(jumpCount<=0)//isGround가 false이거나 jumpCount가 0이면 함수 종료
        {
            return;
        }
        if(jumpCount==1)
        {
            anim.SetBool("isDouble",true);
        }
        rigid.AddForce(new Vector2(0,8.0f),ForceMode2D.Impulse);
        jumpCount--;
    }
    public void Attack()
    {
        anim.SetBool("isAttack",true);
    }
    public void Crouch()
    {
        anim.SetBool("isCrouch",true);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            jumpCount=2;
        }
    }
}
