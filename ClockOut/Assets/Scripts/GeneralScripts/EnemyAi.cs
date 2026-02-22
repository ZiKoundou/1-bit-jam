using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator enemyAnimator;
    [SerializeField] private float attackRange = 2f; // distance at which enemy stops and attacks
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;
    public bool stop = false;
    float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowAI();
    }

    void FollowPlayer()
    {
        agent.SetDestination(player.position);
    }
    void FollowAI()
    {
        if(stop)return;
        distance = Vector3.Distance(player.position, transform.position);
        if (distance > attackRange){
            // Player is far → follow
            agent.isStopped = false;
            FollowPlayer();
        }
        else {
            // Player is in range → stop moving and attack
            agent.isStopped = true;
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
    {
        //trigger attack animator thing
        enemyAnimator.SetTrigger("Attack");
        //player.GetComponent<Health>().TakeDamage(damage);
        // player.GetComponent<Knockback>().ApplyKnockback(transform.position,knockbackForce);
    }
    
}
