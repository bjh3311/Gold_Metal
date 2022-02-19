using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public float mapSpeed=0f;

    private void Update() 
    {
        //mapSpeed만큼 -x축으로 이동
        transform.Translate(-mapSpeed*Time.deltaTime,0,0);
    }
}
