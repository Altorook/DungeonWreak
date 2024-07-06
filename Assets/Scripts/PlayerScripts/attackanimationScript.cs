using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackanimationScript : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    Animator sanimate;
  
    void Start()
    {

        sanimate = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        if (sanimate == null)
        {
            return; // Exit if Animator is not found
        }

      
    }
    private void OnEnable()
    {
        inputManager.onUseRMB += OnUseRMB;
        inputManager.onUseLMB += OnUseLMB;
    }

    private void OnDisable()
    {
        inputManager.onUseRMB -= OnUseRMB;
        inputManager.onUseLMB -= OnUseLMB;
    }
    private void OnUseLMB(bool lMB)
    {
        if (lMB)
        {
            sanimate.SetBool("isSweeping", true);
            sanimate.SetBool("isStabbing", false);
        }
        else
        {
           
            sanimate.SetBool("isSweeping", false);
            sanimate.SetBool("isStabbing", false);
        }

    }
    private void OnUseRMB(bool rMB)
    {
       
         if (rMB)
        {
            sanimate.SetBool("isSweeping", false);
            sanimate.SetBool("isStabbing", true);
        }
        else
        {
            
            sanimate.SetBool("isSweeping", false);
            sanimate.SetBool("isStabbing", false);
        }
    }
}
