using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
   
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private CanvasHandler canvasHandler;
    [SerializeField] private InventoryDisplay inventoryDisplay;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private TMP_Text chestText;
    [SerializeField] private List<int> itemPool = new List<int>();
    [SerializeField] private List<int> itemsInChest = new List<int>();

    // Item amounts
    private readonly Dictionary<int, int> itemAmounts = new Dictionary<int, int>
    {
        {0, 8}, // Bones
        {1, 6}, // Meat
        {2, 5}, // Leather
        {3, 3}, // Iron
        {4, 2}, // Swords
        {5, 5}, // Copper
        {6, 7}, // Wood
        {7, 4}, // Cloth
        {8, 5}, // Tea
        {9, 1}, // Painting
        {10, 3}, // Spork
        {11, 3} // Magazines
    };

   
    private const int MinItemsInChest = 3;
    private const int MaxItemsInChest = 5;
    private int amountInChest;
    private bool lootCoolDown = false;
    private float lootCoolDownTime = 1.0f;
    private float lastLootedTime = 0f;

   
    void Start()
    {
        
        if (inventoryCanvas == null)
        {
            inventoryCanvas = GameObject.Find("InventoryCanvas");
        }
        if (inventoryDisplay == null && inventoryCanvas != null)
        {
            inventoryDisplay = inventoryCanvas.GetComponent<InventoryDisplay>();
        }

        if (canvasObject == null)
        {
            canvasObject = GameObject.Find("Canvas");
        }
        if (canvasHandler == null && canvasObject != null)
        {
            canvasHandler = canvasObject.GetComponent<CanvasHandler>();
        }

       
        GenerateNewPool();

        
        amountInChest = Random.Range(MinItemsInChest, MaxItemsInChest);
        for (int i = 0; i < amountInChest; i++)
        {
            itemsInChest.Add(itemPool[Random.Range(0, itemPool.Count)]);
        }
    }

    private void GenerateNewPool()
    {
        itemPool.Clear();

        foreach (var item in itemAmounts)
        {
            for (int i = 0; i < item.Value; i++)
            {
                itemPool.Add(item.Key);
            }
        }
    }

    
    private void OnTriggerStay(Collider other)
    {
        
        if (other == null || other.gameObject.tag != "Player") return;

        canvasHandler.IsNearChest = true;
        

        if (lootCoolDown && Time.time - lastLootedTime < lootCoolDownTime) return;

        if (Input.GetKey(KeyCode.E))
        {
            LootChest(other.gameObject);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other == null || other.gameObject.tag != "Player") return;

        
        canvasHandler.IsNearChest = false;
    }

  
    private void LootChest(GameObject player)
    {
        if (itemsInChest.Count == 0) return;

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null) return;

        int itemId = itemsInChest[0];
        playerController.inventory[itemId] += 1;
        itemsInChest.RemoveAt(0);

        if (inventoryDisplay != null && inventoryDisplay.isActiveAndEnabled)
        {
            inventoryDisplay.updateInventory();
        }

        lastLootedTime = Time.time;
        lootCoolDown = true;

        if (itemsInChest.Count == 0)
        {
            canvasHandler.IsNearChest = false;
            Destroy(gameObject);
        }
    }

}
