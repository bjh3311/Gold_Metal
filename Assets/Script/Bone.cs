using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    BoxCollider2D box;
    private void Awake() 
    {
        box=this.gameObject.GetComponent<BoxCollider2D>();
    }
   private void OnTriggerEnter2D(Collider2D col)
   {
       if(col.gameObject.CompareTag("Weapon"))
       {
           box.enabled=false;//박스를 끈다
       }
   }
}
