using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] GameObject ARMesh;
    public bool IsActive;

    void Start()
    {   
        ARMesh=gameObject;
        IsActive=false;
        ARMesh.SetActive(false);
        
    }

    public void Scan(){
        if(IsActive==true){
            ARMesh.SetActive(false);
            IsActive=false;
        }else{
            ARMesh.SetActive(true);
            IsActive=true;
        }
    }
    

    // Update is called once per frame
   
}
