using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [Header("Ranged Enemy Settings")]
    public float patrolRadius = 10f;
    public float attackCooldown = 2f;
    public float attackDistance = 8f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [HideInInspector] public float lastAttackTime;

    private RangedPatrolState patrolState;
    private RangedChaseState chaseState;
    private RangedAttackState attackState;

    protected override void Start()
    {
        base.Start();

        // Initialize states with patrolRadius passed in
        patrolState = new RangedPatrolState(this, patrolRadius);
        chaseState = new RangedChaseState(this);
        attackState = new RangedAttackState(this);

        stateMachine.ChangeState(patrolState);
    }


    public void SwitchToChase() => stateMachine.ChangeState(chaseState);
    public void SwitchToPatrol() => stateMachine.ChangeState(patrolState);
    public void SwitchToAttack() => stateMachine.ChangeState(attackState);
}
