using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 
public class Dissolve : MonoBehaviour 
{
    private SpriteRenderer Sr; 
    private float threshold;
    [SerializeField]
    private float dissolveSpeed;
    private void Start() 
    { 
        Sr=this.gameObject.GetComponent<SpriteRenderer>();
        threshold = Sr.material.GetFloat("_Threshold");
    }
    public void UpdateDissolve()
    {
        while(true)
        {      
            StartCoroutine("ActiveDissolve");
            Sr.material.SetFloat("_Threshold", threshold);
            if(threshold>1.01)
            {
                break;
            }   
        }
           
    } 
    IEnumerator ActiveDissolve()
    {
        Debug.Log("dis");
        threshold+=0.01f;
        yield return new WaitForSeconds(10f);
    }
    public void UpdateUnDissolve() 
    {    
        while(true)
        {
            StartCoroutine("ActiveUnDissolve");
            Sr.material.SetFloat("_Threshold", threshold); 
            if(threshold<0)
            {
                break;
            }
        }
    }   
    IEnumerator ActiveUnDissolve()
    {
        threshold-=0.01f;
        yield return new WaitForSeconds(10f);
    }  
}