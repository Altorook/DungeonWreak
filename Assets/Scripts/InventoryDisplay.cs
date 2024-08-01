using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
   public GameObject inventorySlotPrefab;
    [SerializeField]
    GameObject playerObject;
    public List<GameObject> itemList = new List<GameObject>();
    PlayerController playerController;


    
    // Start is called before the first frame update
    void Start()
    {
        
        playerController = playerObject.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
      
    }
    public void updateInventory()
    {
        int indexOfThis = 0;
        int forLoopCount = itemList.Count;

        for (int i = 0; i < forLoopCount; i++)
        {
            Destroy(itemList[i]);
        }
        itemList.Clear();
        int indexKMS = -1;
        foreach (int i in playerController.inventory)
        {
            indexKMS++;
            if(playerController.inventory.ElementAt(indexKMS) != 0)
            {
                itemList.Add(Instantiate(inventorySlotPrefab));
                itemList.ElementAt(indexOfThis).transform.SetParent(this.transform);
                itemList.ElementAt(indexOfThis).GetComponent<InventoryItemData>().setInfo(indexKMS, playerController.inventory.ElementAt(indexKMS));
                itemList.ElementAt(indexOfThis).transform.GetChild(1).GetComponent<TMP_Text>().SetText( getItemName(indexKMS)+" "+"{0}",playerController.inventory.ElementAt(indexKMS));
                indexOfThis++;
               
            }
        }

    }
   public string getItemName(int itemNumID)
    {
        switch (itemNumID)
        {
            case 0:
                return "Bone";
            case 1:
                return "Meat?";
            case 2:
                return "Hide";
            case 3:
                return "Iron";
            case 4:
                return "Broken Swords";
            case 5:
                return "Copper";
            case 6:
                return "Wood";
            case 7:
                return "Cloth";
            case 8:
                return "Tea";
            case 9:
                return "Painting";
            case 10:
                return "Spork";
            case 11:
                return "\"Magazines\"";
            default:
                return "UNKNOWN";
        }
    }
}
