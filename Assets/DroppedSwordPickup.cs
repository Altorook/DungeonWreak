using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedSwordPickup : MonoBehaviour
{

    public GameObject usableItemObject;
    public UsableItems usableItems;
    public int idOfThis = 1;
    // Start is called before the first frame update
    void Start()
    {
        usableItemObject = GameObject.Find("Hotbar");
        usableItems = usableItemObject.GetComponent<UsableItems>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other) 
    {
     
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (usableItems.AddNewItem(idOfThis))
                {
                    Destroy(this.gameObject.transform.parent.transform.parent.gameObject);
                }
            }
        }
    }
}
