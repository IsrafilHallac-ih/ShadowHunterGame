using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    public int EnemyHealth = 100;

    [Header("NavMesh Ayarlarý")]
    public NavMeshAgent enemyAgent;
    public Transform player;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    [Header("PATROLLÝNG - Devriye")]
    public Vector3 walkPoint;
    public float walkPointRange;
    public bool walkPointSet;

    public float sightRange, attackRange;
    public bool EnemySightRange, EnemyAttackRange;


    [Header("ATTACK - Atak")]
    public float attackDelay;
    public bool isAttack;
    public Transform attackPoint;
    public GameObject attackTarget;
    public float targetForce=18f;
    public Animator enemyAnimator;

    


    // Start is called before the first frame update
    void Start()
    {
        enemyAgent=GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemySightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        EnemyAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!EnemySightRange && !EnemyAttackRange)
        {
            // Patrolling Koruduðu Yerde Kordinatlar Hesaplanacak
            Patrolling();
            enemyAnimator.SetBool("PlayerWalking", true);
            enemyAnimator.SetBool("PlayerAttacking", false);
            enemyAnimator.SetBool("PlayerDetecting", false);
        }
        else if (EnemySightRange && !EnemyAttackRange)
        {
            DetectPlayer();
            //Detecting
            enemyAnimator.SetBool("PlayerWalking", false);
            enemyAnimator.SetBool("PlayerAttacking", false);
            enemyAnimator.SetBool("PlayerDetecting", true);
        }
        else if (EnemySightRange && EnemyAttackRange)
        {
            AttackPlayer();
            // AttackPlayer
            enemyAnimator.SetBool("PlayerWalking", false);
            enemyAnimator.SetBool("PlayerAttacking", true);
            enemyAnimator.SetBool("PlayerDetecting", false);
        }

    }

    void Patrolling()
    {
        if (walkPointSet==false)
        {
            float randomZPos = Random.Range(-walkPointRange, walkPointRange);
            float randomXPos = Random.Range(-walkPointRange, walkPointRange);

            walkPoint= new Vector3(transform.position.x + randomXPos, transform.position.y, transform.position.z + randomZPos);
            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            {
                walkPointSet = true;
            }
            

        }
        if (walkPointSet == true)
        {
            enemyAgent.SetDestination(walkPoint);
        }
        Vector3 ToWalkPoint = transform.position - walkPoint;
        if (ToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void DetectPlayer()
    {
       enemyAgent.SetDestination(player.position);
       transform.LookAt(player);
    }

    void AttackPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (isAttack == false)
        {

            Rigidbody rb=Instantiate(attackTarget,attackPoint.position,Quaternion.identity).GetComponent<Rigidbody>(); 
            rb.AddForce(transform.forward*targetForce,ForceMode.Impulse);
            isAttack = true;
            Invoke("ResetAttack", attackDelay);
        }
    }
    void ResetAttack()
    {
        isAttack=false;
    }
    public void EnemyTakeDamage(int DamageAmount)
    {
        EnemyHealth -= DamageAmount;
        if (EnemyHealth <= 0)
        {
            EnemyDeath();
        }
    }
    void EnemyDeath()
    {
        Destroy(gameObject);    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
