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
        if (sanimate == null)
        {
            return; // Exit if Animator is not found
        }

        if (Input.GetMouseButton(0))
        {
            sanimate.SetBool("isSweeping", true);
            sanimate.SetBool("isStabbing", false);
        }
        else if (Input.GetMouseButton(1))
        {
            sanimate.SetBool("isSweeping", false);
            sanimate.SetBool("isStabbing", true);
        }
        else
        {
            // Reset animations when no mouse button is pressed
            sanimate.SetBool("isSweeping", false);
            sanimate.SetBool("isStabbing", false);
        }
    }
}
