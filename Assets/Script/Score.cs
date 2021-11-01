using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score=0;

    private void Start() {
        StartCoroutine("plus");
    }

    // Update is called once per frame
    IEnumerator plus()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            score++;
        }
        
    }
}
