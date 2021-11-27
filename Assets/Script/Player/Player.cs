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
        cameraShake=GameManager.GetComponent<CameraShake>();
        saveScript=BestScore.GetComponent<SaveScore>();
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
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            jumpCount=2;
        }
        if(col.gameObject.CompareTag("Obstacle"))//장애물에 부딪히면
        {
            rigid.velocity=new Vector2(0,10.0f);
            box.isTrigger=true;
            saveScript.Save();
            cameraShake.Shake();
        }
        if(col.gameObject.CompareTag("Item"))
        {
            saveScript.score++;
            col.gameObject.SetActive(false);
        }
    }
    void OnBecameInvisible()//화면에서 안보이면 SaveScore후 카메라 쉐이크 진행
    {
        saveScript.Save();
        cameraShake.Shake();        
    }
}
