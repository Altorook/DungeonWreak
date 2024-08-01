using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlBread : MonoBehaviour
{
    public int currentBreadUses;
    [SerializeField]
    GameObject hotbar;
    UsableItems usableItems;

    [SerializeField]
    GameObject playerCapsule;
    PlayerController playerController;

    [SerializeField]
    GameObject breadCrumb;
    // Start is called before the first frame update
    void Start()
    {
        playerController = playerCapsule.GetComponent<PlayerController>();
        usableItems = hotbar.GetComponent<UsableItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentBreadUses > 0)
        {
            playerController.playerHealth += 10;
            currentBreadUses--;
            usableItems.ChangeBreadUses(currentBreadUses);
        }
        if (Input.GetMouseButtonDown(1) && currentBreadUses > 0)
        {
            Instantiate(breadCrumb, playerCapsule.transform.position + new Vector3(1, 1, 0), Quaternion.identity);
            currentBreadUses--;
            usableItems.ChangeBreadUses(currentBreadUses);
        }
    }
}
