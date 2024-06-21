using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackanimationScript : MonoBehaviour
{
    Animator sanimate;
// Start is called before the first frame update
void Start()
    {
        sanimate = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            sanimate.SetBool("isSweeping", true);
            sanimate.SetBool("isStabbing", false);
        }
        
        if (Input.GetMouseButton(1) )
        {
            sanimate.SetBool("isSweeping", false);
            sanimate.SetBool("isStabbing", true);
        }
    }
}
