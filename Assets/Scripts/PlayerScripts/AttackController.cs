using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class AttackController : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    public GameObject SweepHitBox;
    public GameObject StabHitBox;

    private bool isAttacking = false;
    private bool doAttack = false;

    private float attackTime = 0;
    private float attackDuration = 0;
    private GameObject currentHitBox;

    private DetectHitSweep detectSweep;
    private DetectHitStab detectStab;

    private const float activationDelay = 0.15f;

    void Start()
    {
        Animator bswordfbx = GetComponent<Animator>();
        detectSweep = SweepHitBox.GetComponent<DetectHitSweep>();
        detectStab = StabHitBox.GetComponent<DetectHitStab>();
    }

    private void OnEnable()
    {
        inputManager.onUseRMB += OnUseRMB;
        inputManager.onUseLMB += OnUseLMB;
    }

    private void OnDisable()
    {
        inputManager.onUseRMB -= OnUseRMB;
        inputManager.onUseLMB -= OnUseLMB;
    }

    private void OnUseLMB(bool lMB)
    {
        if (lMB && !isAttacking)
        {
            InitiateAttack(StabHitBox, 0.6f);
        }
    }

    private void OnUseRMB(bool rMB)
    {
        if (rMB && !isAttacking)
        {
            InitiateAttack(SweepHitBox, 0.5f);

        }

    }

    private void InitiateAttack(GameObject hitBox, float duration)
    {
        isAttacking = true;
        doAttack = true;
        attackTime = 0;
        attackDuration = duration;
        currentHitBox = hitBox;
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTime += Time.deltaTime;

            if (attackTime > activationDelay && doAttack)
            {
                currentHitBox.SetActive(true);
                doAttack = false;
            }

            if (attackTime > attackDuration)
            {
                EndAttack();
            }
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        doAttack = false;
        attackTime = 0;
        currentHitBox.SetActive(false);
    }

}
