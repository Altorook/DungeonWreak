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

  
    public List<int> inventory = new List<int>();
    int totalItemsInGame = 20;

   public bool isSprinting = false;

    public float playerHealth = 100;
    TMP_Text healthText;
    // Start is called before the first frame update


    GameObject inventoryCanvas;
    InventoryDisplay inventoryDisplay;
    bool isInventoryOpen = false;
    bool inventoryOpen = false;
    float timeOpened = 0;

    void Start()
    {
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        inventoryDisplay = inventoryCanvas.GetComponent<InventoryDisplay>();

        healthText = GameObject.Find("PlayerHealth").GetComponent<TMP_Text>();
        for(int i = 0; i < totalItemsInGame; i++)
        {
            inventory.Add(0);
        }
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.SetText(playerHealth.ToString());
        HandleSprint();
        mouseYRotation -= Input.GetAxis("Mouse Y") * mouseSens;
        mouseYRotation = Mathf.Clamp(mouseYRotation, -45, 45);
        playerTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSens, 0));
        camTransform.eulerAngles = new Vector3(mouseYRotation, playerTransform.eulerAngles.y, 0);

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryOpen = !isInventoryOpen;
        }
        if (Input.GetKey(KeyCode.Tab) && Time.time - timeOpened > 0.1f)
        {
            
            inventoryOpen = true;
            timeOpened = Time.time;
        }
        if(inventoryOpen) 
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
    public void FixedUpdate()
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
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddForce(new Vector3(0, jumpHeight, 0));
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
 
    
   
    
    
    public void SetGrounded(bool groundState)
    {
        isGrounded = groundState;
    }
  

}
