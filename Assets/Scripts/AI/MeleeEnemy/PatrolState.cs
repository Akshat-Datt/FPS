using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private readonly MeleeEnemy enemy;
    private Vector3 patrolTarget;

    public PatrolState(MeleeEnemy enemy) => this.enemy = enemy;

    public void OnEnter()
    {
        SetNewPatrolPoint();
    }

    public void OnUpdate()
    {
        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
            SetNewPatrolPoint();

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distance <= enemy.detectionRange)
            enemy.SwitchToChase();
    }

    public void OnExit() { }

    private void SetNewPatrolPoint()
    {
        Vector3 randomDir = Random.insideUnitSphere * enemy.patrolRadius;
        randomDir += enemy.transform.position;
        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, enemy.patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
            enemy.agent.SetDestination(patrolTarget);
        }
    }
}
