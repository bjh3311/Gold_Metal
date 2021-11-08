using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y<0)
        {
            rb.velocity+=Vector2.up*Physics2D.gravity.y*2.0f*Time.deltaTime;
        }
        else if(rb.velocity.y>0)
        {
            rb.velocity+=Vector2.up*Physics2D.gravity.y*Time.deltaTime;
        }
    }
}
