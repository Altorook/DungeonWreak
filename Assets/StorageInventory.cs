using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StorageInventory : MonoBehaviour
{

    public List<int> storageInventory = new List<int>();
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
    // Start is called before the first frame update
    void Start()
    {

      playerController = playerCapsule.GetComponent<PlayerController>();
      inventoryDisplay = inventoryUI.GetComponent<InventoryDisplay>();

        for (int i = 0; i < playerController.totalItemsInGame; i++)
        {
            storageInventory.Add(0);
        }
        updateInventory();
    }
    public void updateInventory()
    {
        int indexOfThis = 0;
        int forLoopCount = inventoryDisplay.itemList.Count;
        for (int i = 0; i < forLoopCount; i++)
        {
            Destroy(inventoryDisplay.itemList[i]);
        }
        inventoryDisplay.itemList.Clear();
        int indexKMS = -1;
        foreach (int i in playerController.inventory)
        {
            indexKMS++;
            if (playerController.inventory.ElementAt(indexKMS) != 0)
            {
                inventoryDisplay.itemList.Add(Instantiate(inventoryDisplay.inventorySlotPrefab));
                inventoryDisplay.itemList.ElementAt(indexOfThis).transform.parent = this.transform.GetChild(0);
                inventoryDisplay.itemList.ElementAt(indexOfThis).transform.GetChild(0).GetComponent<TMP_Text>().SetText(inventoryDisplay.getItemName(indexKMS) + " " + "{0}", playerController.inventory.ElementAt(indexKMS));
                indexOfThis++;

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
