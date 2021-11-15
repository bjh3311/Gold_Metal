using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    [SerializeField]
    float mapSpeed=10f;

    private void Update() 
    {
        //mapSpeed만큼 -x축으로 이동
        transform.Translate(-mapSpeed*Time.deltaTime,0,0);
    }
}
