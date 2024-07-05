using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUsableItem : MonoBehaviour
{
    TMP_Text itemName;
    public string displayName;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        itemName = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        this.gameObject.GetComponent<Image>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        itemName.SetText(displayName);
        if(isActive)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0,255,0);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }
   public void SetText(string text)
    {
        displayName = text;
    }
}
