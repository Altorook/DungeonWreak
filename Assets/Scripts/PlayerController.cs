using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    }
 void HandleStorageUI()
    {
        storageCanvas.SetActive(true);
        shopCanvas.SetActive(true);
        storageInventory = storageCanvas.GetComponent<StorageInventory>();
        storageInventory.isStorageOpen = !storageInventory.isStorageOpen;
        storageInventory.UpdateStorageInventory();
        storageCanvas.SetActive(storageInventory.isStorageOpen);
        if (storageInventory.isStorageOpen)
        {
            isShopOpen = !storageInventory.isStorageOpen;
        }
        
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.P))
        {
            HandleStorageUI();
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            isShopOpen = !isShopOpen;
            storageCanvas.SetActive(isShopOpen);
            shopCanvas.SetActive(isShopOpen);
            if (isShopOpen)
            {
                storageInventory.isStorageOpen = !isShopOpen;
            }
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
        HandleSprint();
        mouseYRotation -= Input.GetAxis("Mouse Y") * mouseSens;
        mouseYRotation = Mathf.Clamp(mouseYRotation, -45, 45);
        playerTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSens, 0));
        camTransform.eulerAngles = new Vector3(mouseYRotation, playerTransform.eulerAngles.y, 0);

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
    
    private void HandleSprint()
    {
        if(Input.GetKey(KeyCode.LeftShift)&&stamina>0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        if (isSprinting)
        {
            stamina -= 0.3f;
        }
        else if (stamina<maxStamina&&!Input.GetKey(KeyCode.LeftShift))
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
       
        Vector3 relativeVelocity;

        if (isSprinting)
        {
            relativeVelocity = Quaternion.Euler(0, playerTransform.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal") * sprintSpeed, playerRigidbody.velocity.y, Input.GetAxis("Vertical") * sprintSpeed);
        }
        else
        {
            relativeVelocity = Quaternion.Euler(0, playerTransform.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal") * playerSpeed, playerRigidbody.velocity.y, Input.GetAxis("Vertical") * playerSpeed);
        }
        playerRigidbody.velocity = relativeVelocity;

    }


}
