using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonBall : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Boss"))
        {
            Destroy(this.gameObject);
        }
    }
}
