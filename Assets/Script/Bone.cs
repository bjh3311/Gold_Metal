using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    BoxCollider2D box;
    Animator anim;
    AudioSource Audio;
    private void Awake() 
    {
        box=this.gameObject.GetComponent<BoxCollider2D>();
        anim=this.gameObject.GetComponent<Animator>();
        Audio=this.gameObject.GetComponent<AudioSource>();
    }
   private void OnCollisionEnter2D(Collision2D col) 
   {
       if(col.gameObject.CompareTag("Weapon"))
       {
           Audio.Play();
           box.enabled=false;//박스를 끈다
           anim.SetBool("isBreak",true);
       }
   }
   public void Off()
   {
       this.gameObject.SetActive(false);//부서지면 끈다
   }
}
