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
        if(itemNumID == 0)
        {
            return "Bone";
        }else if(itemNumID == 1)
        {
            return "Meat?";
        }else if (itemNumID == 2)
        {
            return "Hide";
        }else if (itemNumID == 3)
            {
            return "Iron";
        }else if (itemNumID == 4)
        {
            return "Broken Swords";
        }
        else if (itemNumID == 5)
        {
            return "Copper";
        }
        else if (itemNumID == 6)
        {
            return "Wood";
        }
        else if (itemNumID == 7)
        {
            return "Cloth";
        }
        else if (itemNumID == 8)
        {
            return "Tea";
        }
        else if (itemNumID == 9)
        {
            return "Painting";
        }
        else if (itemNumID == 10)
        {
            return "Spork";
        }
        else if (itemNumID == 11
            )
        {
            return "\"Magazines\"";
        }
        else
        {
            return "UNKNOWN";
        }
    }
}
