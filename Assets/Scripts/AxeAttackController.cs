using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttackController : MonoBehaviour
{
    public GameObject AxeSweepHitBox;
    public GameObject AxeOverheadHitBox;
    private bool isAttacking = false;
    private bool sweepAttack = false;
    private bool stabAttack = false;
    private float sweepAttackTime = 0;
    private float stabAttackTime = 0;
    private bool doAttack = false;
    private float sweepTime = 1.5f;
    private float stabTime = 1.4f;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame


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
    void Update()
    {
        
        if (stabAttack)
        {
            stabAttackTime += Time.deltaTime;
        }
        if (stabAttackTime > .15f && doAttack)
        {
            AxeOverheadHitBox.SetActive(true);
            doAttack = false;


        }
        if (stabAttackTime > stabTime)
        {
            isAttacking = false;
            stabAttack = false;
            doAttack = false;
            stabAttackTime = 0;
            AxeOverheadHitBox.SetActive(false);

        }


       
        if (sweepAttack)
        {
            sweepAttackTime += Time.deltaTime;
        }
        if (sweepAttackTime > .15f && doAttack)
        {
            AxeSweepHitBox.SetActive(true);
            doAttack = false;


        }
        if (sweepAttackTime > sweepTime)
        {
            isAttacking = false;
            sweepAttack = false;
            doAttack = false;
            sweepAttackTime = 0;
            AxeSweepHitBox.SetActive(false);
        }

    }

}
