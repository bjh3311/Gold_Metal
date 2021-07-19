using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movePower = 5f;
    public float jumpPower = 5f;
    // Start is called before the first frame update
    Rigidbody2D rigid;

    Animator animator;

    Vector3 movement;
    bool isJumping = false;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal")<0)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);//Left Flip
            animator.SetBool("isMoving", true);
            
        }
        else if(Input.GetAxisRaw("Horizontal")>0)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
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
