using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHitStab : MonoBehaviour
{

    public float hitCoolDown;
    EnemyMovementV1 otherEnemyMovement;
    private float stabDamageMin = 20f;
    private float stabDamageMax = 35f;
    private float damageDealt;
    public float stabAttackTime = 0.2f;
    public float stabAttackCurrentTime = 0;
    public bool isAttacking = false;
    Animator swordattack;
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
        if (isAttacking)
        {
            stabAttackCurrentTime += Time.deltaTime;
        }
        if(stabAttackCurrentTime > stabAttackTime) 
        {
            isAttacking = false;
            stabAttackCurrentTime = 0;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (stabAttackCurrentTime < stabAttackTime && isAttacking == false)
            {
                otherEnemyMovement = other.gameObject.GetComponent<EnemyMovementV1>();
                damageDealt = Random.Range(stabDamageMin, stabDamageMax)*playerController.damageBoost;
                otherEnemyMovement.OnStabHit(damageDealt);
                isAttacking = true;
            }
        }
        
        
    }
}
