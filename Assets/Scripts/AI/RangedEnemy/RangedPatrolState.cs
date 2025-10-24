// PatrolState.cs
using UnityEngine;
using UnityEngine.AI;

public class RangedPatrolState : IState
{
    private readonly EnemyBase enemy;
    private readonly float patrolRadius;
    private Vector3 patrolTarget;

    public RangedPatrolState(EnemyBase enemy, float patrolRadius)
    {
        this.enemy = enemy;
        this.patrolRadius = patrolRadius;
    }

    public void OnEnter()
    {
        SetNewPatrolPoint();
    }

    public void OnUpdate()
    {
        if (enemy == null || enemy.agent == null) return;

        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
            SetNewPatrolPoint();

        if (enemy.player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distance <= enemy.detectionRange)
        {
            // state switch handled in ChaseState or by concrete enemy if needed
            // Try to switch to a chase/attack state via concrete enemy methods:
            if (enemy is MeleeEnemy melee) melee.SwitchToChase();
            else if (enemy is RangedEnemy ranged) ranged.SwitchToChase();
        }
    }

    public void OnExit() { }

    private void SetNewPatrolPoint()
    {
        Vector3 randomDir = Random.insideUnitSphere * patrolRadius;
        randomDir += enemy.transform.position;
        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
            enemy.agent.SetDestination(patrolTarget);
        }
    }
}
