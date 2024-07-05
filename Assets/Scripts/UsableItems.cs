using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UsableItems : MonoBehaviour
{
    
    [SerializeField]
    public GameObject[] itemSlots = new GameObject[5];


    [SerializeField]
    public GameObject[] itemsToUse = new GameObject[5];

    [SerializeField]
    List<GameObject> itemsInGame = new List<GameObject>();

    [SerializeField]
    List<GameObject> droppedItemPrefabs = new List<GameObject>();

    [SerializeField]
    GameObject playerCapsule;

    public DisplayUsableItem[] itemSlotScript = new DisplayUsableItem[5];
    public int[] usableItemArray = new int[5];
    public int activeItemSlot = 1;
    public int checkIfItemChanged;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(usableItemArray.Length + " " + itemSlots.Length + " " + itemSlotScript.Length); ;
        for (int i = 0; i < itemSlotScript.Length; i++)
        {
            itemSlotScript[i] = itemSlots[i].GetComponent<DisplayUsableItem>();
            usableItemArray[i] = 10;
        }
        usableItemArray[0] = 1;
        usableItemArray[1] = 2;
        usableItemArray[2] = 3;
        usableItemArray[3] = 4;
        usableItemArray[4] = 5;
        for (int i = 0; i < itemSlotScript.Length; i++)
        {
            itemSlotScript[i].SetText(getUsableItemName(usableItemArray[i]));
        }
        SetActive(activeItemSlot - 1);
        checkIfItemChanged = activeItemSlot;
    }

    // Update is called once per frame
    void Update()
    {
        DropItem();
        
        if(checkIfItemChanged != activeItemSlot)
        {
            changeItem(activeItemSlot -1 ,checkIfItemChanged-1);

            checkIfItemChanged = activeItemSlot;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            activeItemSlot = 1;
            SetActive(activeItemSlot-1);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            activeItemSlot = 2;
            SetActive(activeItemSlot - 1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            activeItemSlot = 3;
            SetActive(activeItemSlot - 1);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            activeItemSlot = 4;
            SetActive(activeItemSlot - 1);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            activeItemSlot = 5;
            SetActive(activeItemSlot - 1);
        }
    }
    void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (usableItemArray[activeItemSlot-1]!= 0) 
            {
                GameObject newDrop = Instantiate(droppedItemPrefabs.ElementAt(usableItemArray[activeItemSlot - 1] - 1), playerCapsule.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                newDrop.transform.GetChild(0).transform.GetChild(0).GetComponent<DroppedSwordPickup>().idOfThis = usableItemArray[activeItemSlot - 1];
                usableItemArray[activeItemSlot - 1] = 0;
            }

            itemsToUse[activeItemSlot - 1].SetActive(false);
            if (itemsToUse[activeItemSlot - 1].name != "Empty")
            {
                itemsToUse[activeItemSlot - 1] = new GameObject("Empty");
            }
            itemSlotScript[activeItemSlot-1].SetText(getUsableItemName(usableItemArray[activeItemSlot - 1]));

        }
    }
    public bool AddNewItem(int thisID)
    {
        bool openSpot = false;

        if (usableItemArray[activeItemSlot-1] == 0)
        {
            openSpot = true;
            itemsToUse[activeItemSlot - 1].SetActive(false);
            usableItemArray[activeItemSlot - 1] = thisID;
        }
        else
        {
            for (int i = 0; i < itemsToUse.Length; i++)
            {
                if (usableItemArray[i] == 0)
                {
                    openSpot = true;
                    usableItemArray[i] = thisID;
                    break;
                }
            }
        }
        UpdateItemsInHotbar();
        itemsToUse[activeItemSlot - 1].SetActive(true);
        return openSpot;
    }
    void SetActive(int i)
    {
        for(int j = 0; j < itemSlotScript.Length; j++)
        {
            if (j == i)
            {
                itemSlotScript[j].isActive = true;
            }
            else
            {
                itemSlotScript[j].isActive = false;
            }
        }
    }
    void UpdateItemsInHotbar()
    {
        for(int i = 0; i<usableItemArray.Length; i++)
        {
            itemsToUse[i] = itemsInGame.ElementAt(usableItemArray[i]);
            itemSlotScript[i].SetText(getUsableItemName(usableItemArray[i]));
        }
    }
    void changeItem(int active, int lastItem)
    {
        itemsToUse[lastItem].SetActive(false);
        itemsToUse[active].SetActive(true);
       
    }
    public string getUsableItemName(int itemNumID)
    {
        if (itemNumID == 0)
        {
            return "No Item";
        }
        if (itemNumID == 1)
        {
            return "Starting Sword";
        }
        else if (itemNumID == 2)
        {
            return "Starting Axe";
        }
        else if (itemNumID == 3)
        {
            return "Torch";
        }
        else if (itemNumID == 4)
        {
            return "Bread";
        }
        else if (itemNumID == 5)
        {
            return "Wine";
        }
        else if (itemNumID == 6)
        {
            return "Cheese";
        }
        else
        {
            return "Unknown";
        }

    }
}
