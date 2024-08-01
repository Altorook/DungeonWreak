using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDetectOverhead : MonoBehaviour
{
    public float hitCoolDown;
    EnemyMovementV1 otherEnemyMovement;
    private float sweepDamageMin = 23f;
    private float sweepDamageMax = 45f;
    private float damageDealt;
    GameObject playerCapsule;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerCapsule = GameObject.Find("PlayerCapsule");
        playerController = playerCapsule.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            otherEnemyMovement = other.gameObject.GetComponent<EnemyMovementV1>();
            damageDealt = Random.Range(sweepDamageMin, sweepDamageMax)*playerController.damageBoost;
            otherEnemyMovement.OnSweepHit(damageDealt);
        }
    }
}
