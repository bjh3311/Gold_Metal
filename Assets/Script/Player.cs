using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movePower = 5f;
    public float jumpPower = 5f;
    // Start is called before the first frame update
    Rigidbody2D rigid;
    SpriteRenderer rend;
    Animator animator;

    Vector3 movement;
    bool isJumping = false;
    public GameObject bulletObj;
    private Transform transform;
    
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        transform = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }
    public void Detected()
    {
        Debug.Log("주인공이 시야에 들어왔습니다!!");
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
        Fire();    
    }  
    void Fire()
    {
        if(!Input.GetButtonDown("Fire2"))//마우스 오른쪽버튼이 안눌려있다면 종료
        {
            return;
        }
       if(rend.flipX)//왼쪽을 본다면
       {
            GameObject bullet = Instantiate(bulletObj, transform.position + Vector3.left * 2.0f + Vector3.up * 0.1f, transform.rotation);
            //현재 위치보다 오른쪽위에 총알생성 
            Rigidbody2D rigid_bullet = bullet.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        }
       else
       {
            GameObject bullet = Instantiate(bulletObj, transform.position + Vector3.right * 2.0f + Vector3.up * 0.1f, transform.rotation);
            //현재 위치보다 오른쪽위에 총알생성 
            Rigidbody2D rigid_bullet = bullet.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
        }
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal")<0)
        {
            moveVelocity = Vector3.left;
            rend.flipX=true;//Left Flip
            animator.SetBool("isMoving", true);
            
        }
        else if(Input.GetAxisRaw("Horizontal")>0)
        {
            moveVelocity = Vector3.right;
            rend.flipX = false; 
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        transform.position += moveVelocity * movePower*Time.deltaTime;
    }
    void Jump()
    {
        if(!isJumping)
        {
            return;
        }
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        isJumping = false;
    }
}
