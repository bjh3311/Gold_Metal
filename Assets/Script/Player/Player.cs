using System.Collections;
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
        Debug.Log("무기 박스 켜기");
    }
    public void WeaponBoxOff()//boxcollider 끄기
    {
        weaponBox.enabled=false;
        Debug.Log("무기 박스 끄기");
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
            MapMove.mapSpeed=0;
        }
        if(col.gameObject.CompareTag("Bone"))//부술수 있는 뼈들과 부딪히면
        {
            box.isTrigger=true;
            saveScript.Save();
            cameraShake.Shake();
            saveScript.StopCoroutine("plus");
            MapMove.mapSpeed=0;
        }
        if(col.gameObject.CompareTag("Item"))
        {
            saveScript.score++;
            col.gameObject.SetActive(false);
        }
    }
}
