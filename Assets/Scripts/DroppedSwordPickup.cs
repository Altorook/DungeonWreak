using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedSwordPickup : MonoBehaviour
{

    public GameObject usableItemObject;
    public UsableItems usableItems;
    public int idOfThis = 1;
    public int breadUses = 9;
    public int breadUsesFromSource;
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (usableItems.AddNewItem(idOfThis,breadUses))
                {
                    Destroy(this.gameObject.transform.parent.transform.parent.gameObject);
                }
            }
        }
    }
}
