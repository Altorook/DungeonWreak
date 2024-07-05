using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    GameObject canvasObject;
    CanvasHandler canvasHandler;
    int amountOfBones = 8;
    int amountOfMeat = 6;
    int amountOfLeather = 5;
    int amountOfIron = 3;
    int amountOfSwords = 2;
    int amountOfCopper = 5;
    int amountOfWood = 7;
    int amountOfCloth = 4;
    int amountOfTea = 5;
    int amountOfPainting = 1;
    int amountOfSpork = 3;
    int amountOfMagazines = 3;
    [SerializeField]
    List<int> itemPool = new List<int>();
    public List<int> itemsInChest= new List<int>();
    int minItemsInChest = 3;
    int maxItemsInChest = 5;
    int amountInChest;
    int framesSinceLooted = 0;
    bool lootCoolDown = false;
    InventoryDisplay inventoryDisplay;
    GameObject inventoryCanvas;
    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        inventoryDisplay = inventoryCanvas.GetComponent<InventoryDisplay>();

        canvasObject = GameObject.Find("Canvas");
        canvasHandler = canvasObject.GetComponent<CanvasHandler>();
        generateNewPool();
        amountInChest = Random.Range(minItemsInChest, maxItemsInChest);
        for (int i = 0; i < amountInChest; i++)
        {
            itemsInChest.Add(itemPool.ElementAt(Random.Range(0, itemPool.Count)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void generateNewPool()
    {itemPool.Clear();
       
        for(int i = 0; i < amountOfBones; i++) 
        {
            itemPool.Add(0);
        }
        for (int i = 0; i < amountOfMeat; i++)
        {
            itemPool.Add(1);
        }
        for (int i = 0; i < amountOfLeather; i++)
        {
            itemPool.Add(2);
        }
        for (int i = 0; i < amountOfIron; i++)
        {
            itemPool.Add(3);
        }
        for (int i = 0; i < amountOfSwords; i++)
        {
            itemPool.Add(4);
        }
        for (int i = 0;i < amountOfCopper; i++)
        {
            itemPool.Add(5);
        }
        for (int i = 0; i < amountOfWood; i++)
        {
            itemPool.Add(6);
        }
        for (int i = 0; i < amountOfCloth; i++)
        {
            itemPool.Add(7);
        }
        for (int i = 0; i < amountOfTea; i++)
        {
            itemPool.Add(8);
        }
        for (int i = 0; i < amountOfPainting; i++)
        {
            itemPool.Add(9);
        }
        for (int i = 0; i < amountOfSpork; i++)
        {
            itemPool.Add(10);
        }
        for (int i = 0; i < amountOfMagazines; i++)
        {
            itemPool.Add(11);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Player")
            {
                this.GetComponentInChildren<TMP_Text>().SetText("OI");
                canvasHandler.IsNearChest = true;
                if (lootCoolDown)
                {
                    framesSinceLooted++;
                    if(framesSinceLooted > 23)
                    {
                        framesSinceLooted = 0;
                        lootCoolDown = false;
                    }
                }
                if (Input.GetKey(KeyCode.E) && lootCoolDown == false)
                {
                    
                    
                    lootCoolDown = true;
                    PlayerController playCont = other.gameObject.GetComponent<PlayerController>();
                    playCont.inventory[itemsInChest.ElementAt(0)]+=1;
                    itemsInChest.RemoveAt(0);
                    if (inventoryDisplay.isActiveAndEnabled)
                    {   
                        inventoryDisplay.updateInventory();
                    }
                    if (itemsInChest.Count == 0)
                    {
                        canvasHandler.IsNearChest = false;
                        Destroy(this.gameObject); 
                    }
                   
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Player")
            {
                this.GetComponentInChildren<TMP_Text>().SetText("");
                canvasHandler.IsNearChest = false;
            }
        }
    }

}
