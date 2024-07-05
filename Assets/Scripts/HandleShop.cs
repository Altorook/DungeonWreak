using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HandleShop : MonoBehaviour
{
    [SerializeField]
    GameObject gameManagerObject;
    GameManager gameManager;

    [SerializeField]
    GameObject playerCapsule;
    PlayerController playerController;

    [SerializeField]
    GameObject inventoryUI;
    InventoryDisplay inventoryDisplay;

    public int[] priceArray = new int[20];
    public int[] purchasePriceArray = new int[5];


    int boneMin = 1;
    int boneMax = 5;
    int meatMin = 2;
    int meatMax = 5;
    int hideMin = 3;
    int hideMax = 8;
    int ironMin = 5;
    int ironMax = 7;
    int brokenSwordMin = 3;
    int brokenSwordMax = 15;
    int copperMin = 6;
    int copperMax = 11;
    int woodMin = 4;
    int woodMax = 8;
    int clothMin = 9;
    int clothMax = 13;
    int teaMin = 6;
    int teaMax = 17;
    int paintingMin = 25;
    int paintingMax = 60;
    int sporkMin = 10;
    int sporkMax = 12;
    int magazineMin = 1;
    int magazineMax = 100;

    int starterSwordMin = 20;
    int starterSwordMax = 40;
    int starterAxeMin = 35;
    int starterAxeMax = 65;
    int torchMin = 25;
    int torchMax = 35;
    int breadMin = 60;
    int breadMax = 75;
    int wineMin = 100;
    int wineMax = 120;
    [SerializeField]
    GameObject storageCanvas;
    public List<GameObject> shopInventory = new List<GameObject>();
    public List<GameObject> itemsForSaleSlots = new List<GameObject>();
    [SerializeField]
    List<GameObject> droppedItemPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        

    }
    void OnEnable()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        playerController = playerCapsule.GetComponent<PlayerController>();
        inventoryDisplay = inventoryUI.GetComponent<InventoryDisplay>();
        SetPricesForDay();
        UpdateShopInventory();
        for(int i = 0; i <itemsForSaleSlots.Count; i++)
        {
            itemsForSaleSlots.ElementAt(i).GetComponent<ShopSaleSlot>().thisSlotID = i;
        }
    }
    public void PurchaseItem(int itemID)
    {
        if(gameManager.GetGold() * 100 + gameManager.GetSilver() >= purchasePriceArray[itemID])
        {
            GameObject newPurchase = Instantiate(droppedItemPrefabs.ElementAt(itemID),playerCapsule.transform.position + new Vector3(0,1,0), Quaternion.identity);
            newPurchase.transform.GetChild(0).transform.GetChild(0).GetComponent<DroppedSwordPickup>().idOfThis = itemID+1;
            gameManager.SetSilver(gameManager.GetSilver() - purchasePriceArray[itemID]);
        }
    }


    public void UpdateShopInventory()
    {
        int indexOfThis = 0;
        int forLoopCount = shopInventory.Count;
        storageCanvas.SetActive(true);
        for (int i = 0; i < forLoopCount; i++)
        {
            Destroy(shopInventory[i]);
        }
        shopInventory.Clear();
        int indexKMS = -1;
        foreach (int i in playerController.inventory)
        {
            indexKMS++;
            if (playerController.inventory.ElementAt(indexKMS) != 0)
            {
                shopInventory.Add(Instantiate(inventoryDisplay.inventorySlotPrefab));
                shopInventory.ElementAt(indexOfThis).transform.SetParent(this.transform.GetChild(0));
                shopInventory.ElementAt(indexOfThis).GetComponent<InventoryItemData>().setInfo(indexKMS, playerController.inventory.ElementAt(indexKMS));
                shopInventory.ElementAt(indexOfThis).transform.GetChild(2).GetComponent<TMP_Text>().SetText("{0} Silver", priceArray[indexKMS]);
                shopInventory.ElementAt(indexOfThis).transform.GetChild(1).GetComponent<TMP_Text>().SetText(inventoryDisplay.getItemName(indexKMS) + " " + "{0}", playerController.inventory.ElementAt(indexKMS));
                indexOfThis++;

            }
        }
       storageCanvas.SetActive(false);
    }
    public void SetPricesForDay()
    {
        for(int i = 0; i < priceArray.Length; i++)
        {
            if(i == 0)
            {
                priceArray[i] = Random.Range(boneMin,boneMax+1);
            }
            if (i == 1)
            {
                priceArray[i] = Random.Range(meatMin, meatMax + 1);
            }
            if (i == 2)
            {
                priceArray[i] = Random.Range(hideMin, hideMax + 1);
            }
            if (i == 3)
            {
                priceArray[i] = Random.Range(ironMin, ironMax + 1);
            }
            if (i == 4)
            {
                priceArray[i] = Random.Range(brokenSwordMin, brokenSwordMin);
            }
            if (i == 5)
            {
                priceArray[i] = Random.Range(copperMin, copperMax + 1);
            }
            if (i == 6)
            {
                priceArray[i] = Random.Range(woodMin, woodMax + 1);
            }
            if (i == 7)
            {
                priceArray[i] = Random.Range(clothMin, clothMax + 1);
            }
            if (i == 8)
            {
                priceArray[i] = Random.Range(teaMin, teaMax + 1);
            }
            if (i == 9)
            {
                priceArray[i] = Random.Range(paintingMin, paintingMax + 1);
            }
            if (i == 10)
            {
                priceArray[i] = Random.Range(sporkMin, sporkMax + 1);
            }
            if (i == 11)
            {
                priceArray[i] = Random.Range(magazineMin, magazineMax + 1);
            }
            if (i == 12)
            {
                priceArray[i] = 0;
            }
            if (i == 13)
            {
                priceArray[i] = 0;
            }
            if (i == 14)
            {
                priceArray[i] = 0;
            }
            if (i == 15)
            {
                priceArray[i] = 0;
            }
            if (i == 16)
            {
                priceArray[i] = 0;
            }
            if (i == 17)
            {
                priceArray[i] = 0;
            }
            if (i == 18)
            {
                priceArray[i] = 0;
            }
            if (i == 19)
            {
                priceArray[i] = 0;
            }
        }
        for(int i = 0; i < purchasePriceArray.Length; i++)
        {
            if(i == 0)
            {
                purchasePriceArray[i] = Random.Range(starterSwordMin,starterSwordMax+1);
                this.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().SetText("S {0}", purchasePriceArray[i]);
            }
            if (i == 1)
            {
                purchasePriceArray[i] = Random.Range(starterAxeMin, starterAxeMax + 1);
                this.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().SetText("S {0}", purchasePriceArray[i]);
            }
            if (i == 2)
            {
                purchasePriceArray[i] = Random.Range(torchMin, torchMax + 1);
                this.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().SetText("S {0}", purchasePriceArray[i]);
            }
            if (i == 3)
            {
                purchasePriceArray[i] = Random.Range(breadMin, breadMax + 1);
                this.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().SetText("S {0}", purchasePriceArray[i]);
            }
            if (i == 4)
            {
                purchasePriceArray[i] = Random.Range(wineMin, wineMax + 1);
                this.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().SetText("S {0}", purchasePriceArray[i]);
            }

        }
    }
    public void SellItem(int thisID)
    {
        Debug.Log("sold: " + thisID.ToString());
        gameManager.SetSilver(gameManager.GetSilver() + priceArray[thisID]);
        playerController.inventory[thisID]--;
        UpdateShopInventory();
    }


    // Update is called once per frame
    void Update()
    {
    }
}
