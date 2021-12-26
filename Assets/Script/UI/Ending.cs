using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 
public class Ending : MonoBehaviour 
{
    private SpriteRenderer Sr; 
    private float threshold;
    [SerializeField]
    private float dissolveTime;
    private void Start() 
    { 
        Sr=this.gameObject.GetComponent<SpriteRenderer>();
        threshold = Sr.material.GetFloat("_Threshold");
    }
    public void UpdateDissolve()
    {
        StartCoroutine("ActiveDissolve");    
    } 
    IEnumerator ActiveDissolve()
    {
        while(true)
        {
            threshold+=0.1f;
            yield return new WaitForSeconds(dissolveTime);
            Sr.material.SetFloat("_Threshold", threshold);
            if(threshold>1.01)
            {
                break;
            }
        }
        
    }
}