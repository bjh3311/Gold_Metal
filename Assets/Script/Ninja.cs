﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
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
        box=this.gameObject.GetComponent<BoxCollider2D>();
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
        rigid.velocity=new Vector2(0,8.0f);
        jumpCount--;
    }
    public void Attack()
    {
        anim.SetBool("isAttack",true);
        
    }
    public void AttackEnd()
    {
        anim.SetBool("isAttack",false);
    }
    public void CrouchStart()//Crouch 누르는 동안 crouch
    {
        anim.SetBool("isCrouch",true);
        box.size=new Vector2(1.5f,1f);
        box.offset=new Vector2(0,-0.5f);
    }
    public void CrouchEnd()//Crouch떼면 바로 Run
    {
        anim.SetBool("isCrouch",false);
        box.size=new Vector2(1.5f,2f);
        box.offset=new Vector2(0,0);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        anim.SetBool("isDouble",false);
        if(col.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            jumpCount=2;
        }
    }
}
