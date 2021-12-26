using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI;
public class Ending : MonoBehaviour 
{
    private Image Im; 
    private float threshold=1.01f;
    [SerializeField]
    private float dissolveTime;
    private void Start() 
    {
        Im=this.gameObject.GetComponent<Image>();
        Im.material.SetFloat("_Threshold", 1.01f);
    }
    public void UpdateUnDissolve()
    {
        StartCoroutine("ActiveUnDissolve");    
    } 
    IEnumerator ActiveUnDissolve()
    {
        while(true)
        {
            Im.material.SetFloat("_Threshold", threshold);
            threshold-=0.1f;
            yield return new WaitForSeconds(dissolveTime);    
            if(threshold<=0.0f)
            {
                break;
            }
        }
        
    }
}