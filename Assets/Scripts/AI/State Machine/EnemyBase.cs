using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    public string enemyType = "MeleeEnemy"; // must match pool name
    public float maxHealth = 100f;
    public float currentHealth;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float damage = 10f;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Transform player;
    protected StateMachine stateMachine;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void Die()
    {
        if (EnemyPool.Instance != null)
        {
            EnemyPool.Instance.ReturnEnemy(enemyType, this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
