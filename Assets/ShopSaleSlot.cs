using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSaleSlot : MonoBehaviour
{
    public int thisSlotID;
    public GameObject shopCanvas;
    public HandleShop handleShop;

    // Start is called before the first frame update
    void Start()
    {
        shopCanvas = GameObject.Find("ShopUI");
        handleShop = shopCanvas.GetComponent<HandleShop>();
    }
    public void PurchasedThisItem()
    {
        handleShop.PurchaseItem(thisSlotID);
    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
