using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

enum AiState { Patrol, Chase, Flank, GetBehind}
public class EnemyMovementV1 : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject playerObject;

    //empty gameObjects on either side of player for the flank mechanic
    GameObject flankLeft;
    GameObject flankRight;

    //distance to lose player
    float distanceToLose = 50;

    //Rough distance for enemy attacks
    public float attackDistance = 3.3f;

    //list of the points the enemy patrols(Is generated automatically
    public List<GameObject> patrolPoints = new List<GameObject>();
    //useless but neccessary
    public int numberOfPoints = 0;

    //current patrol point the enemy will go to
    public int currentPatrolPoint;

    //has to do with IDLE. Start timer is bool that allows timeIdled to increase. This is used to see how much time the enemy spent idling before it is sent to the next patrol point
    public bool startTimer = false;
    public float timeToIdle = 4;
    public float timeIdled = 0;

    //speeds (Bhenchode)
    [SerializeField] float chaseSpeed = 9;
    [SerializeField] float patrolSpeed = 2;

    enum AiState { Patrol, Chase, Attack,Flank } // ...

    //idrk but switch state stuff ig
    [SerializeField]
    AiState state = AiState.Patrol;

    //bool that stores if the enemy is actively idling
    public bool isFlanking = false;

    //stores which side the enemy will flank if isFlanking is true
    public bool isFlankingLeft;

    //tells the AI it can switch sides it is flanking
    bool changeFlank;

    public float health = 100;
    public float timeLastHit = 0;
    public float timeSinceLastHit = 1f;
    private float attackDamageMin = 5;
    private float attackDamageMax = 15;
    private float attackSpeed = 1.1f;
    private float attackTime = 0;
    [SerializeField]
    TMP_Text healthText;
    // Start is called before the first frame update
    Animator anim;
  
    void Start()
    {
    
        anim = GetComponentInChildren<Animator>();

       //starts the enemies on a random patrol point
        currentPatrolPoint = Random.Range(0, patrolPoints.Count - 1);

        //self explanatory
        playerObject = GameObject.FindGameObjectWithTag("Player");
        flankLeft = GameObject.Find("FlankLeft");
        flankRight = GameObject.Find("FlankRight");
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        //creates the list of patrol points automatically. This is very delicate so try not to change much involving this. If having issues probably crtl z till its better
        foreach (Transform child in this.transform.parent.transform.parent)
        {
            numberOfPoints++;
            string nameOf = string.Format("EnemyPatrolPoint ({0})", numberOfPoints);
            if(this.transform.parent.transform.parent.Find(nameOf) != null)
            {
                patrolPoints.Add(this.transform.parent.transform.parent.Find(nameOf).gameObject);
            }
           
        }
       
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationState();

        float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
       if ((this.transform.position - playerObject.transform.position).magnitude < attackDistance + 2)
        {
            if (attackTime <= attackSpeed)
            {
                attackTime += Time.deltaTime;
            }
            else if (attackTime >= attackSpeed)
            {
                DoAttack();
                attackTime = 0;

            }
        }
        else
        {
            attackTime = 0;
        }
       

        healthText.SetText(((int)health).ToString());
        if(health<=0)
        {
            Destroy(this.transform.parent.gameObject);
        }



        timeSinceLastHit = (Time.time - timeLastHit);
        //if player is closer than distance to lose (Also to alert, same thing) decide based  on isFlanking to either flank or standard chase. If further than distance to lose, revert to patrol
        if((this.transform.position - playerObject.transform.position).magnitude < distanceToLose)
        {

            if (isFlanking)
            {
                state = AiState.Flank;
            }
            else
            {
                state = AiState.Chase;
            }          
        }
        else
        {
           
            state = AiState.Patrol;
        }


        //switch state stuff
        switch (state)
        {
            case AiState.Patrol:
                PatrolState();
                
                break;
            case AiState.Chase:
                ChasePlayer();
                if (distanceToPlayer <= attackDistance)
                {
                    state = AiState.Attack;
                }
                FacePlayer();
                break;
            case AiState.Flank:
                FlankPlayer();
                FacePlayer();
                break;
            case AiState.Attack:
                AttackPlayer();
                FacePlayer();
                if (distanceToPlayer > attackDistance)
                {
                    state = AiState.Chase;
                }
               
                break;
        }
    }
    void AttackPlayer()
    {
        if (attackTime <= attackSpeed)
        {
            attackTime += Time.deltaTime;
        }
        else
        {
            attackTime = 0;
            DoAttack();
        }
    }
    public void DoAttack()
    {
        float attackDamage;
        attackDamage = Random.Range(attackDamageMin, attackDamageMax);
       PlayerController playerContoller = playerObject.GetComponent<PlayerController>();
        playerContoller.playerHealth -= attackDamage;
       

    }
    public void OnSweepHit(float damageDone)
    {
        if(timeSinceLastHit >= .5f)
        {
            timeLastHit = Time.time;
            health -= damageDone;
        }
    }
    public void OnStabHit(float damageDone)
    {
        if(timeSinceLastHit>= 0.5f)
        {
            timeLastHit = Time.time;
            health -= damageDone;
        }
    }
    private void ChasePlayer()
    {
        //when enemy switched back to chase, allow enemy to change flank side next time they flank
        changeFlank = true;
        agent.speed = chaseSpeed;

        //randomly switch to flanking
        if(Random.Range(0,1000) == 200)
        {
            anim.SetTrigger("StartFlank");
            isFlanking = true;
           
        }
      
        //chase player
        if ((this.transform.position - playerObject.transform.position).magnitude < distanceToLose && (this.transform.position - playerObject.transform.position).magnitude > attackDistance)
        {
            agent.destination = playerObject.transform.position;
            
        }
        //close enough to attack. This is where to tell the enemy to attack!!!!!!!!!!!
        else if ((this.transform.position - playerObject.transform.position).magnitude <= attackDistance)
        {
            //actually somewhere in here but you get it
            agent.destination = this.transform.position;
        }
        else
        {
            agent.destination = this.transform.position;
        }
    }
    private void FacePlayer()
    {
       
        Vector3 directionToPlayer = playerObject.transform.position - this.transform.position;
        directionToPlayer.y = 0; 

       
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

    }
    private void PatrolState()
    {
        agent.speed = patrolSpeed;
        //tells the ai which patrol point to head to
        agent.destination = patrolPoints.ElementAt(currentPatrolPoint).transform.position;
       
        //checks if the enemy is close enough to a patrol point to start to idle
        if ((this.transform.position - patrolPoints.ElementAt(currentPatrolPoint).transform.position).magnitude < 3)
        {
            startTimer = true;

        }
        //holds how long the player idles for
        if (startTimer)
        {
            timeIdled += Time.deltaTime;
        }
        //resets idle state and the timer
        if (timeIdled > timeToIdle)
        {
            startTimer = false;
            timeIdled = 0;
            currentPatrolPoint = Random.Range(0, patrolPoints.Count-1);
        }
    }
    private void FlankPlayer()
    {

        //randomly 50/50 change flank side
        if (changeFlank)
        {
            if (Random.Range(0, 2) == 1)
            {
                isFlankingLeft = true;
            }
            else
            {
                isFlankingLeft = false;
            }
            //stops the enemy from constatly changing sides
            changeFlank = false;
        }
        agent.speed = chaseSpeed;
        //randomly switches back to chase standard
        if (Random.Range(0, 3000) == 20)
        {
            isFlanking = false;
            anim.SetTrigger("StopFlank");
        }
        if ((this.transform.position - playerObject.transform.position).magnitude < distanceToLose && (this.transform.position - playerObject.transform.position).magnitude > attackDistance)
        {
           
            if (isFlankingLeft)
            {
                agent.destination = flankLeft.transform.position;
            }
            else
            {
                agent.destination = flankRight.transform.position;
            }
            
        }
        if ((this.transform.position - flankLeft.transform.position).magnitude < attackDistance)
        {
            anim.SetTrigger("StopFlank");
            isFlanking = false;
        }
         if ((this.transform.position - flankRight.transform.position).magnitude < attackDistance)
        {
            anim.SetTrigger("StopFlank");
            isFlanking = false;
        }
        
    }
    private void UpdateAnimationState()
    {
        anim.SetBool("isWalking", state == AiState.Patrol);
        anim.SetBool("isRunning", state == AiState.Chase);
        anim.SetBool("isAttacking", state == AiState.Attack);
    }
}
