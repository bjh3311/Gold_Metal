using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowLong : MonoBehaviour
{
    // Start is called before the first frame update
    Image nowWhere;
    private float dis=0;
    void Start()
    {
        nowWhere=this.gameObject.GetComponent<Image>();
        StartCoroutine("plus");
    }

    // Update is called once per frame
    void Update()
    {
        nowWhere.fillAmount=dis/100f;
    }
    IEnumerator plus()
    {
        while(true)
        {
            if(Time.timeScale!=0)
            {
                dis=dis+0.1f;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
