using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TMP_Text chestEText;
    [SerializeField]
    GameObject ChestProximity;
    public bool IsNearChest = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsNearChest)
        {
            ChestProximity.SetActive(true);
        }
        else
        {
            ChestProximity.SetActive(false);
        }
    }
 
}
