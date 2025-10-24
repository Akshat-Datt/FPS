using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [Header("Melee Enemy Settings")]
    public float patrolRadius = 8f;
    public float attackCooldown = 1.5f;

    [HideInInspector] public float lastAttackTime;

    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;

    protected override void Start()
{
    base.Start();

    // Initialize states with patrolRadius passed in
    patrolState = new PatrolState(this, patrolRadius);
    chaseState  = new ChaseState(this);
    attackState = new AttackState(this);

    stateMachine.ChangeState(patrolState);
}


    public void SwitchToChase() => stateMachine.ChangeState(chaseState);
    public void SwitchToPatrol() => stateMachine.ChangeState(patrolState);
    public void SwitchToAttack() => stateMachine.ChangeState(attackState);
}
