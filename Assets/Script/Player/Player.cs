﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


public class Player : MonoBehaviour
{
    public GameObject GM;
    private CameraShake cameraShake;
    Rigidbody2D rigid;
    BoxCollider2D box;//Player boxCollider
    BoxCollider2D weaponBox;//무기 boxCollider
    public GameObject weapon;//무기
    Animator anim;
    private int jumpCount=2;//2번까지 점프
    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
    private void Start() 
    {
        rigid=this.gameObject.GetComponent<Rigidbody2D>();
        anim=this.gameObject.GetComponent<Animator>();
        box=this.gameObject.GetComponent<BoxCollider2D>();
        cameraShake=GM.GetComponent<CameraShake>();
        weaponBox=weapon.GetComponent<BoxCollider2D>();
    }
    public void Jump()
    {
        if(jumpCount<=0)//isGround가 false이거나 jumpCount가 0이면 함수 종료
        {
            return;
        }
        rigid.velocity=new Vector2(0,10.0f);
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
    public void WeaponBoxOn()//boxcollider 켜기, 무기를 휘드를때만 boxCollider가 켜져 있게 한다.
    {
        weaponBox.enabled=true;
    }
    public void WeaponBoxOff()//boxcollider 끄기
    {
        weaponBox.enabled=false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            jumpCount=2;
        }
        if(col.gameObject.CompareTag("Obstacle"))//장애물에 부딪히면
        {       
            box.isTrigger=true;
            cameraShake.Shake();
            GameManager.instance.MapMove.mapSpeed=0;
            GameManager.instance.HowLong.end=true;//끝났다
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Board"))//하단 경계선에 부딪히면
        {       
            box.isTrigger=true;
            cameraShake.Shake();
            GameManager.instance.MapMove.mapSpeed=0;
            GameManager.instance.HowLong.end=true;//끝났다
        }
    }
}
