using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 
public class Dissolve : MonoBehaviour 
{
    private SpriteRenderer Sr; 
    private float threshold;

    private void Start() 
    { 
        Sr=this.gameObject.GetComponent<SpriteRenderer>();
        threshold = Sr.material.GetFloat("_Threshold");
    }
    private void UpdateDissolve(float dissolveSpeed)
    {
        while(true)
        {
            threshold -= Time.deltaTime / dissolveSpeed; 
            Sr.material.SetFloat("_Threshold", threshold);
            if(threshold<0)
            {
                this.gameObject.SetActive(false);
                break;
            }   
        }
           
    } 
    private void UpdateUnDissolve(float dissolveSpeed) 
    {    
        this.gameObject.SetActive(true);
        while(true)
        {
            threshold += Time.deltaTime / dissolveSpeed; 
            Sr.material.SetFloat("_Threshold", threshold); 
            if(threshold>1.01)
            {
                break;
            }
        }
    }     
}