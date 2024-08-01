using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEditor.Build;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform camTransform;
    [SerializeField]
    Rigidbody playerRigidbody;
    [SerializeField]
    GameObject playerObject;

    public float damageRes = 1;
    public float damageBoost = 1;
    float wineDuration = 25;
    float timeForWine = 0;
    bool wineDrank = false;


    [SerializeField]
    float playerSpeed = 7;
    [SerializeField]
    float sprintSpeed = 12;
    public float stamina = 1000;
    [SerializeField]
    float maxStamina = 1000;

    [SerializeField]
    float mouseSens = 4;
    [SerializeField]
    public bool isGrounded = false;
    [SerializeField]
    int jumpHeight = 150;
    [SerializeField]
    float mouseYRotation = 0;

    [SerializeField]
    GameObject gameManagerObject;
    GameManager gameManager;
    public List<int> inventory = new List<int>();
   public int totalItemsInGame = 20;

   public bool isSprinting = false;

    public float playerHealth = 100;
    float checkIfHealthChanged;
    TMP_Text healthText;
    // Start is called before the first frame update


    GameObject inventoryCanvas;
    InventoryDisplay inventoryDisplay;
   public bool isInventoryOpen = false;
    bool inventoryOpen = false;
    float timeOpened = 0;

    public bool menuOpen = false;
    [SerializeField]
    GameObject storageCanvas;
    StorageInventory storageInventory;

    [SerializeField]
    GameObject shopCanvas;
   public bool isShopOpen = false;

    bool canOpenShop = false;
    bool canOpenStorage = false;
    public bool canLootChest = false;
    bool canGetInBed = false;
    ResetEnemyChest REC;

    public GameObject chestCollidedWith;

    [SerializeField]
    GameObject canvasObject;

    Vector3 relativeVelocity;
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        gameManager.SetHealth(playerHealth);
        checkIfHealthChanged = playerHealth;
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        inventoryDisplay = inventoryCanvas.GetComponent<InventoryDisplay>();

        healthText = GameObject.Find("PlayerHealth").GetComponent<TMP_Text>();
        for(int i = 0; i < totalItemsInGame; i++)
        {
            inventory.Add(0);
        }

        storageCanvas.SetActive(true);
        storageInventory = storageCanvas.GetComponent<StorageInventory>();
        storageCanvas.SetActive(false);

        REC = this.gameObject.GetComponent<ResetEnemyChest>();
    }

    public void InteractWith()
    {
        if (canOpenStorage)
        {
            HandleStorageUI();
        }
        if (canOpenShop)
        {
            isShopOpen = !isShopOpen;
            storageCanvas.SetActive(isShopOpen);
            shopCanvas.SetActive(isShopOpen);
            if (isShopOpen)
            {
                storageInventory.isStorageOpen = !isShopOpen;
            }
        }
        if (canLootChest)
        {
            chestCollidedWith.GetComponent<ChestControl>().LootFromChest(this.gameObject);
        }
        if (canGetInBed)
        {
            REC.HandleReset();
            Debug.Log("Why");
        }
    }
    public void WineDrank()
    {
        timeForWine = 0;
        wineDrank = true;
    }
    void WineEffect()
    {
        damageBoost = 1.5f;
        damageRes = 1.5f;
        timeForWine += Time.deltaTime;
        if(timeForWine > wineDuration)
        {
            wineDrank = false;
            damageRes = 1;
            damageBoost = 1;
        }
    }
    void HandleStorageUI()
    {

        storageInventory = storageCanvas.GetComponent<StorageInventory>();
        storageInventory.isStorageOpen = !storageInventory.isStorageOpen;
        storageInventory.UpdateStorageInventory();
        storageCanvas.SetActive(storageInventory.isStorageOpen);
        if (storageInventory.isStorageOpen)
        {
            isShopOpen = !storageInventory.isStorageOpen;
        }
        
    }

  /*  public void Rotation(Vector2 input)
    {
        if (menuOpen == false)
        {
            HandleSprint();
            mouseYRotation -= input.y * mouseSens;
            mouseYRotation = Mathf.Clamp(mouseYRotation, -45, 45);
            mouseXRotation = input.x;
            playerTransform.Rotate(new Vector3(0, mouseXRotation * mouseSens, 0));
            camTransform.eulerAngles = new Vector3(mouseYRotation, playerTransform.eulerAngles.y, 0);
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if(wineDrank)
        {
            WineEffect();
        }

        if (storageInventory.isStorageOpen || isShopOpen || isInventoryOpen)
        {
            menuOpen = true;
        }
        else
        {
            menuOpen = false;
        }

        if(checkIfHealthChanged != playerHealth)
        {
            gameManager.SetHealth(playerHealth);
            checkIfHealthChanged = playerHealth;
        }
      
         
        if (menuOpen)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }


        healthText.SetText(playerHealth.ToString());
        if(menuOpen == false)
        {
            HandleSprint();
            //cant figure out how to make the input system work smoothly for the mouse
            mouseYRotation -= Input.GetAxis("Mouse Y") * mouseSens;
            mouseYRotation = Mathf.Clamp(mouseYRotation, -45, 45);
            playerTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSens, 0));
            camTransform.eulerAngles = new Vector3(mouseYRotation, playerTransform.eulerAngles.y, 0);
        }
        HandleInventory();
    }
    public void HandleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryOpen = !isInventoryOpen;
            menuOpen = !menuOpen;
        }
        if (Input.GetKey(KeyCode.Tab) && Time.time - timeOpened > 0.1f)
        {

            inventoryOpen = true;
            timeOpened = Time.time;
        }
        if (inventoryOpen)
        {
            inventoryDisplay.updateInventory();
            inventoryOpen = false;
        }
        if (isInventoryOpen)
        {

            inventoryCanvas.SetActive(true);


        }
        else
        {
            inventoryCanvas.SetActive(false);
        }
    }
    
    public void EnableSprint()
    {
        isSprinting = true;
    }
    public void DisableSprint()
    {
        isSprinting = false;
    }
    private void HandleSprint()
    {
        if (isSprinting)
        {
            stamina -= 0.3f;
        }
        else if (stamina<maxStamina&&isSprinting == false)
        {
            stamina += 0.25f; 
        }
        if(stamina>maxStamina) 
        {
            stamina = maxStamina;
        }
        if(stamina<=0)
        {
            stamina=0;
            isSprinting = false;
        }
    }

    private void FixedUpdate()
    {
        if (isSprinting)
        {
            playerRigidbody.velocity = Quaternion.Euler(0, playerTransform.eulerAngles.y, 0) * (relativeVelocity * sprintSpeed) + new Vector3(0, playerRigidbody.velocity.y, 0);
        }
        else
        {
            playerRigidbody.velocity = Quaternion.Euler(0, playerTransform.eulerAngles.y, 0) * relativeVelocity + new Vector3(0,playerRigidbody.velocity.y,0) ;
        }
    }
    public void Movement(Vector2 input)
    {
        
        if (menuOpen == false)
        {
       
                relativeVelocity = new Vector3(input.x * playerSpeed, 0, input.y * playerSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shop")
        {
            canvasObject.SetActive(true);
            canOpenShop = true;
        }
        else if(other.tag == "Storage")
        {
            canvasObject.SetActive(true);
            canOpenStorage = true;
        }
        else if (other.tag == "Bed")
        {
            canvasObject.SetActive(true);
            canGetInBed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            canvasObject.SetActive(false);
            canOpenShop = false;
        }
        else if (other.tag == "Storage")
        {
            canvasObject.SetActive(false);
            canOpenStorage = false;
        }
        else if( other.tag == "Bed")
        {
            canvasObject.SetActive(false);
            canGetInBed = false;
        }
    }

}
