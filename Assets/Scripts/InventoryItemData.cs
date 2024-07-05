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
        storageCanvas = GameObject.Find("StorageBoxInventory");
        playerCapsule = GameObject.Find("PlayerCapsule");
        shopCanvas = GameObject.Find("ShopUI");

        storageInventory = storageCanvas.GetComponent<StorageInventory>();
        playerController = playerCapsule.GetComponent<PlayerController>();
        handleShop = shopCanvas.GetComponent<HandleShop>();
    }
    private void OnEnable()
    {
        storageCanvas = GameObject.Find("StorageBoxInventory");
        playerCapsule = GameObject.Find("PlayerCapsule");
        shopCanvas = GameObject.Find("ShopUI");

        storageInventory = storageCanvas.GetComponent<StorageInventory>();
        playerController = playerCapsule.GetComponent<PlayerController>();
        handleShop = shopCanvas.GetComponent<HandleShop>();
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
            Debug.Log("inv");
        }
        else if(this.transform.parent.name == "ShopInventory")
        {
            
           handleShop.SellItem(itemID);
            
        }
    }
}
