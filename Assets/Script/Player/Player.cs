﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject GameManager;
    private CameraShake cameraShake;
    public GameObject BestScore;
    public GameObject NowScore;
    private SaveScore saveScript;
    public GameObject Ground;
    private MapMove MapMove;
    Rigidbody2D rigid;
    BoxCollider2D box;//Player boxCollider
    BoxCollider2D weaponBox;//무기 boxCollider
    public GameObject weapon;//무기
    Animator anim;
    private bool isGround=true;//점프제한을위한 변수
    private int jumpCount=2;//2번까지 점프
    [SerializeField]
    private LayerMask groundLayer;//땅 체크
    private Transform groundCheck;//땅 체크

    private HowLong HowLong;
    public GameObject NowDis;

    private int layerBone;//뼈장애물들을 체크하기위한 layer변수
    private RaycastHit2D boneHit;//뼈에 맞았는지 감지하기 위한 변수
    private void Start() 
    {
        rigid=this.gameObject.GetComponent<Rigidbody2D>();
        anim=this.gameObject.GetComponent<Animator>();
        groundCheck=this.gameObject.GetComponent<Transform>();
        box=this.gameObject.GetComponent<BoxCollider2D>();
        cameraShake=GameManager.GetComponent<CameraShake>();
        saveScript=BestScore.GetComponent<SaveScore>();
        MapMove=Ground.GetComponent<MapMove>();
        weaponBox=weapon.GetComponent<BoxCollider2D>();
        HowLong=NowDis.GetComponent<HowLong>();
    }
    private void FixedUpdate() 
    {
        layerBone=1<<LayerMask.NameToLayer("Bone");
        boneHit=Physics2D.Raycast(transform.position+new Vector3(0.5f,0,0),new Vector3(1.0f,0,0),0.1f,layerBone);
        if(boneHit.collider!=null)//뼈에 맞는다면 탈락
        {
            box.isTrigger=true;
            saveScript.Save();
            cameraShake.Shake();
            saveScript.StopCoroutine("plus");
            MapMove.mapSpeed=0;
        }
        Debug.DrawRay(transform.position+new Vector3(0.5f,0,0),new Vector3(1.0f,0,0)*0.1f,new Color(0,1,0));
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
        if(col.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            jumpCount=2;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Obstacle"))//장애물에 부딪히면
        {       
            box.isTrigger=true;
            saveScript.Save();
            cameraShake.Shake();
            saveScript.StopCoroutine("plus");
            HowLong.StopCoroutine("plus");
            MapMove.mapSpeed=0;
        }
        if(col.gameObject.CompareTag("Item"))
        {
            saveScript.score++;
            col.gameObject.SetActive(false);
        }
    }
}
