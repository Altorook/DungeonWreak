using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHitSweep : MonoBehaviour
{
    public float hitCoolDown;
    EnemyMovementV1 otherEnemyMovement;
    private float sweepDamageMin = 15f;
    private float sweepDamageMax = 25f;
    private float damageDealt;
    // Start is called before the first frame update
    void Start()
    {
        
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
            damageDealt = Random.Range(sweepDamageMin, sweepDamageMax);
            otherEnemyMovement.OnSweepHit(damageDealt);
        }
    }
}
