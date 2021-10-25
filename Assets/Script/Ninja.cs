using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    private void Start() 
    {
        rigid=this.gameObject.GetComponent<Rigidbody2D>();
        anim=this.gameObject.GetComponent<Animator>();
    }
    public void Jump()
    {
        rigid.AddForce(new Vector2(0,3.0f),ForceMode2D.Impulse);//점프
        Debug.Log("점프 버튼 눌름");
    }
    public void Attack()
    {
        Debug.Log("Attack");
    }
    public void Crouch()
    {
        Debug.Log("Crouch");
    }
}
