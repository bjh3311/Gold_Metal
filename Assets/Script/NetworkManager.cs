using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private void Awake() 
    {
        var obj=FindObjectOfType<NetworkManager>();
        if(obj.Length==1)
        {

        }
        
    }
    void Update()
    {
        
    }
}
