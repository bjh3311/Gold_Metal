using System.Collections;
 using System.Collections.Generic;
  using UnityEngine;
   public class Dissolve : MonoBehaviour 
   { 
       bool isUpdateDissolve; 
       bool isUpdateUnDissolve; 
       public SpriteRenderer Sr;
       private float threshold; 
        private void Start() 
        { 
            threshold = Sr.material.GetFloat("_Threshold");//해당 섀이더를 열어보면 해당 프로퍼티의 이름을 알 수 있다.
        }
        private void Update() 
        { 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { 
                 isUpdateDissolve = true;
                  isUpdateUnDissolve = false; 
            } 
            if (Input.GetKeyDown(KeyCode.Alpha2))
             { 
                 isUpdateDissolve = false; 
                 isUpdateUnDissolve = true; 
            } 
                
            if (isUpdateDissolve) 
                 UpdateDissolve(1f);
                  if (isUpdateUnDissolve) 
                  UpdateUnDissolve(1f); 
        } 
        private void UpdateDissolve(float dissolveSpeed) 
        {
             threshold += Time.deltaTime / dissolveSpeed;
              Sr.material.SetFloat("_Threshold", threshold); 
        }
    private void UpdateUnDissolve(float dissolveSpeed) 
    { 
        threshold -= Time.deltaTime / dissolveSpeed; 
        Sr.material.SetFloat("_Threshold", threshold);
         } 
}