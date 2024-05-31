using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    GameObject canvasObject;
    CanvasHandler canvasHandler;
    int amountOfBones = 6;
    int amountOfMeat = 4;
    int amountOfLeather = 3;
    int amountOfIron = 2;
    int amountOfSwords = 1;
    [SerializeField]
    List<int> itemPool = new List<int>();
    public List<int> itemsInChest= new List<int>();
    int minItemsInChest = 3;
    int maxItemsInChest = 5;
    int amountInChest;
    int framesSinceLooted = 0;
    bool lootCoolDown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
    public void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Player")
            {
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
                    if(itemsInChest.Count == 0)
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
                canvasHandler.IsNearChest = false;
            }
        }
    }
}
