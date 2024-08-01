using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineControl : MonoBehaviour
{
    GameObject hotBar;
    UsableItems usableItems;
    GameObject playerCapsule;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerCapsule = GameObject.Find("PlayerCapsule");
        playerController = playerCapsule.GetComponent<PlayerController>();
        hotBar = GameObject.Find("Hotbar");
        usableItems = hotBar.GetComponent<UsableItems>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            usableItems.UseWine();
            playerController.WineDrank();
        } 
    }
}
