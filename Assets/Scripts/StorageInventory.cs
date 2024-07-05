using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StorageInventory : MonoBehaviour
{

    public List<int> storageInventory = new List<int>();
    public List<GameObject> storageItemList = new List<GameObject>();


    [SerializeField]
    GameObject playerCapsule;
     PlayerController playerController;
    [SerializeField]
    GameObject inventoryUI;
     InventoryDisplay inventoryDisplay;
    [SerializeField]
    GameObject storagePanel;
    [SerializeField]
    GameObject inventoryPanel;

    [SerializeField]
    GameObject shopCanvas;
   public bool isStorageOpen = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        playerController = playerCapsule.GetComponent<PlayerController>();
        inventoryDisplay = inventoryUI.GetComponent<InventoryDisplay>();

        for (int i = 0; i < playerController.totalItemsInGame; i++)
        {
            storageInventory.Add(0);
        }
       
    }
    public void UpdateStorageInventory()
    {
      inventoryDisplay.updateInventory();
        int indexOfThis = 0;
        int forLoopCount = inventoryDisplay.itemList.Count;
        for (int i = 0; i < forLoopCount; i++)
        {
            Destroy(inventoryDisplay.itemList[i]);
        }
        forLoopCount = storageItemList.Count;
        for (int i = 0;i < forLoopCount; i++)
        {
            Destroy(storageItemList[i]);
        }
        storageItemList.Clear();
        inventoryDisplay.itemList.Clear();
        int indexKMS = -1;
        foreach (int i in playerController.inventory)
        {
            indexKMS++;
            if (playerController.inventory.ElementAt(indexKMS) != 0)
            {
                inventoryDisplay.itemList.Add(Instantiate(inventoryDisplay.inventorySlotPrefab));
                inventoryDisplay.itemList.ElementAt(indexOfThis).transform.SetParent(this.transform.GetChild(0));
                inventoryDisplay.itemList.ElementAt(indexOfThis).GetComponent<InventoryItemData>().setInfo(indexKMS, playerController.inventory.ElementAt(indexKMS));
                inventoryDisplay.itemList.ElementAt(indexOfThis).transform.GetChild(1).GetComponent<TMP_Text>().SetText(inventoryDisplay.getItemName(indexKMS) + " " + "{0}", playerController.inventory.ElementAt(indexKMS));
                indexOfThis++;

            }
        }
        indexKMS = -1;
        indexOfThis = 0;
        foreach (int i in storageInventory)
        {
            indexKMS++;
            if (storageInventory.ElementAt(indexKMS) != 0)
            {
                storageItemList.Add(Instantiate(inventoryDisplay.inventorySlotPrefab));
                storageItemList.ElementAt(indexOfThis).transform.SetParent(this.transform.GetChild(1));
                storageItemList.ElementAt(indexOfThis).GetComponent<InventoryItemData>().setInfo(indexKMS, storageInventory.ElementAt(indexKMS));
                storageItemList.ElementAt(indexOfThis).transform.GetChild(1).GetComponent<TMP_Text>().SetText(inventoryDisplay.getItemName(indexKMS) + " " + "{0}", storageInventory.ElementAt(indexKMS));
                indexOfThis++;

            }
        }
        shopCanvas.SetActive(false);
    }
   
    public void TransferToStorage(int id)
    {
        storageInventory[id]++;
        playerController.inventory[id]--;
        UpdateStorageInventory();
    }
    public void TransferFromStorage(int id)
    {
        storageInventory[id]--;
        playerController.inventory[id]++;
        UpdateStorageInventory();
    }
    // Update is called once per frame
    void Update()
    {
        
        
       
       
      
    }
}
