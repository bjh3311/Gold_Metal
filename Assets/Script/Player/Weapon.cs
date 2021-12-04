using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    BoxCollider2D box;
    private void Start() 
    {
        box=this.gameObject.GetComponent<BoxCollider2D>();
    }
    public void boxOn()//boxcollider 켜기, 무기를 휘드를때만 boxCollider가 켜져 있게 한다.
    {
        box.enabled=true;
    }
    public void boxOff()//boxcollider 끄기
    {
        box.enabled=false;
    }
}
