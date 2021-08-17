using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public TalkManager manager;
    GameObject scanObject;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        transform = this.gameObject.GetComponent<Transform>();
    }
    private void OnBecameInvisible()//화면 밖으로 나가면 다음 씬으로 잠시 넘긴다
    {
        SceneManager.LoadScene("Test");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        if (Input.GetKey(KeyCode.Z) && scanObject != null)
        {
            manager.Action();
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
        RaycastHit2D rayHit;
        if(rend.flipX)//왼쪽
        {
            rayHit = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0), 10, LayerMask.GetMask("TalkNpc"));
        }
        else//오른쪽
        {
            rayHit = Physics2D.Raycast(transform.position, new Vector3(1, 0, 0), 10, LayerMask.GetMask("TalkNpc"));
        }
        if(rayHit.collider!=null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }     
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
            rigid_bullet.AddForce(Vector2.left * 2, ForceMode2D.Impulse);
        }
       else
       {
            GameObject bullet = Instantiate(bulletObj, transform.position + Vector3.right * 2.0f + Vector3.up * 0.1f, transform.rotation);
            //현재 위치보다 오른쪽위에 총알생성 
            Rigidbody2D rigid_bullet = bullet.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
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