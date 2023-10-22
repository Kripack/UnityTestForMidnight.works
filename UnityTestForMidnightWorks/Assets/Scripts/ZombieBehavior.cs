using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ZombieBehavior : MonoBehaviour
{
    public float lookRadius = 5f;
    public float attackDistance = 1f;
    public float animetAttackDistance = 4f;
    public float attackDelay = 1f;
    public int damage = 20;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float nextAttackTime = 0f;
    private bool oneTime;
    private Rigidbody rb;
    private UIController uiController;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(player.position);
            animator.SetBool("Run", true);
            if (distance <= animetAttackDistance && !oneTime)
            {
                animator.SetBool("Attack", true);
            }
            else if (distance >= animetAttackDistance && !oneTime)
            {
                animator.SetBool("Attack", false);
            }

            if (distance <= attackDistance && !oneTime)
            {
                if (Time.time >= nextAttackTime)
                {                   
                    AttackPlayer();
                    nextAttackTime = Time.time + attackDelay;
                }
            }

        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void AttackPlayer()
    {
        player.GetComponent<PlayerTarget>().TakeDamage(damage);
        
    }

    public void Die()
    {
        if (!oneTime)
        {
            uiController.ZombieDied();
            agent.isStopped = true ;
            animator.SetTrigger("Die");
            Destroy(gameObject, 1f);
            oneTime = true;
        }
        else
        {
            return;
        }
    }
}
