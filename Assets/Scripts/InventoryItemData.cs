using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData : MonoBehaviour
{
     int itemID;
     int itemQuantity;
     public GameObject storageCanvas;
     public GameObject playerCapsule;
     public GameObject shopCanvas;

    public StorageInventory storageInventory;
    public PlayerController playerController;
    public HandleShop handleShop;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        if (GameObject.Find("StorageBoxInventory") != null)
        {
            storageCanvas = GameObject.Find("StorageBoxInventory");
            storageInventory = storageCanvas.GetComponent<StorageInventory>();
        }
        if (GameObject.Find("PlayerCapsule") != null)
        {
            playerCapsule = GameObject.Find("PlayerCapsule");
            playerController = playerCapsule.GetComponent<PlayerController>();
        }
        if (GameObject.Find("ShopUI") != null)
        {


            shopCanvas = GameObject.Find("ShopUI");


            handleShop = shopCanvas.GetComponent<HandleShop>();
        }

    }
    public void setInfo(int id, int quant)
    {
        itemID = id;
        itemQuantity = quant;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void invStorSell()
    {
        if(this.transform.parent.name == "Storage")
        {
            storageInventory.TransferFromStorage(itemID);
        }
        else if(this.transform.parent.name == "Inventory")
        {
            storageInventory.TransferToStorage(itemID);
        }
        else if (this.transform.parent.name == "InventoryCanvas")
        {

        }
        else if(this.transform.parent.name == "ShopInventory")
        {
            
           handleShop.SellItem(itemID);
            
        }
    }
}
