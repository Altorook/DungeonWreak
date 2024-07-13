using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class InventoryScript : MonoBehaviour
{
    
[SerializeField]
private InputManager inputManager;

    private GameObject inventoryCanvas;
    private InventoryDisplay inventoryDisplay;
    public bool isInventoryOpen = false;

    [SerializeField]
    private GameObject storageCanvas;
    private StorageInventory storageInventory;

    [SerializeField]
    private GameObject shopCanvas;
    public bool isShopOpen = false;

    [SerializeField]
    private GameObject gameManagerObject;
    private GameManager gameManager;

    public List<int> inventory = new List<int>(); // Consider initializing with specific items
    public int totalItemsInGame = 20;
    public bool menuOpen = false;
    public float playerHealth = 100;

    public ChestControl currentChest;

    // Start is called before the first frame update
    void Start()
    {
        InitializeInventory();
        InitializeStorage();
        Cursor.visible = false;
    }

    void InitializeInventory()
    {
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        if (inventoryCanvas != null)
        {
            inventoryDisplay = inventoryCanvas.GetComponent<InventoryDisplay>();
            if (inventoryDisplay == null)
            {
                Debug.LogError("Inventory not found on InventoryCanvas.");
            }
        }
        else
        {
            Debug.LogError("InventoryCanvas not found.");
        }

        // Consider initializing inventory with specific items here
        for (int i = 0; i < totalItemsInGame; i++)
        {
            inventory.Add(0);
        }
    }

    void InitializeStorage()
    {
        storageCanvas.SetActive(true);
        storageInventory = storageCanvas.GetComponent<StorageInventory>();
        if (storageInventory == null)
        {
            Debug.LogError("StorageInventory component not found on StorageCanvas.");
        }
        storageCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCursorState();
    }

    void UpdateCursorState()
    {
        if (isShopOpen || storageInventory.isStorageOpen || isInventoryOpen)
        {
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnEnable()
    {
        inputManager.onChestInteract += OpenChest;
        inputManager.onUseRMB += OnStorageUI;
        inputManager.onUseLMB += OnToggleInventory;
        inputManager.onOpenShop += OnOpenShop;
    }

    private void OnDisable()
    {
        inputManager.onChestInteract += OpenChest;

        inputManager.onUseRMB -= OnStorageUI;
        inputManager.onUseLMB -= OnToggleInventory;
        inputManager.onUseLMB -= OnOpenShop;
    }


    private void OpenChest()
    {
        if (currentChest)
        {
            currentChest.LootChest(this.gameObject);
        }
    }
    private void OnStorageUI(bool store)
    {
        if (store)
        {
            bool newState = !storageInventory.isStorageOpen;
            SetStorageActive(newState);
        }
    }

    void SetStorageActive(bool isActive)
    {
        storageInventory.isStorageOpen = isActive;
        storageInventory.UpdateStorageInventory();
        storageCanvas.SetActive(isActive);
        if (isActive)
        {
            SetShopActive(false);
        }
    }

    private void OnOpenShop(bool store)
    {
        if (store)
        {
            bool newState = !isShopOpen;
            SetShopActive(newState);
        }
    }

    void SetShopActive(bool isActive)
    {
        isShopOpen = isActive;
        shopCanvas.SetActive(isActive);
        if (isActive)
        {
            SetStorageActive(false);
        }
        UpdateMenuState();
    }

    private void OnToggleInventory(bool inventory)
    {
        if (inventory)
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryCanvas.SetActive(isInventoryOpen);
            if (isInventoryOpen)
            {
                inventoryDisplay.updateInventory();
            }
        }
        UpdateMenuState();
    }

    void UpdateMenuState()
    {
        menuOpen = storageInventory.isStorageOpen || isShopOpen || isInventoryOpen;
    }



}
