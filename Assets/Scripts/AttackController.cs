using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject SweepHitBox;
    public GameObject StabHitBox;
    private bool isAttacking = false;
    private bool sweepAttack = false;
    private bool stabAttack = false;
    private float sweepAttackTime = 0;
    private float stabAttackTime = 0;
    private bool doAttack = false;
    private float sweepTime = 0.5f;
    private float stabTime = 0.6f;
    private DetectHitSweep  detectSweep;
    private DetectHitStab detectStab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Animator bswordfbx = GetComponent<Animator>();

        detectSweep = SweepHitBox.GetComponent<DetectHitSweep>();
        detectStab = StabHitBox.GetComponent<DetectHitStab>();
    }
    public void LAttack()
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            stabAttack = true;
            doAttack = true;

        }
    }
    public void RAttack()
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            sweepAttack = true;
            doAttack = true;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (stabAttack)
        {
            stabAttackTime += Time.deltaTime;
        }
        if (stabAttackTime > .15f && doAttack)
        {
            StabHitBox.SetActive(true);
            doAttack = false;


        }
        if (stabAttackTime > stabTime)
        {
            isAttacking = false;
            stabAttack = false;
            doAttack = false;
            stabAttackTime = 0;
            StabHitBox.SetActive(false);
          
        }


       
        if(sweepAttack)
        {
            sweepAttackTime += Time.deltaTime;
        }
        if (sweepAttackTime > .15f && doAttack)
        {
            SweepHitBox.SetActive(true);
            doAttack = false;
      
           
        }
        if(sweepAttackTime > sweepTime)
        {
            isAttacking = false;
            sweepAttack = false;
            doAttack = false;
            sweepAttackTime = 0;
            SweepHitBox.SetActive(false);
        }
        
    }

}
