using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject inventorySlotPrefab;
    [SerializeField]
    GameObject playerObject;
    List<GameObject> itemList = new List<GameObject>();
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
        playerController = playerObject.GetComponent<PlayerController>();
        playerController.inventory[0] += 1;
        updateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        playerController.inventory[0] += 1;
        updateInventory();
    }
    public void updateInventory()
    {
        int indexOfThis = 0;
        foreach (int i in playerController.inventory)
        {
            if(playerController.inventory.ElementAt(i) != 0)
            {
                itemList.Add(Instantiate(inventorySlotPrefab));
                itemList.ElementAt(indexOfThis).transform.parent = this.transform;
                indexOfThis++;
            }
        }
    }
}
