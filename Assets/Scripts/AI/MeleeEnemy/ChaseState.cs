using UnityEngine;

public class ChaseState : IState
{
    private readonly MeleeEnemy enemy;

    public ChaseState(MeleeEnemy enemy) => this.enemy = enemy;

    public void OnEnter()
    {
        enemy.agent.isStopped = false;
    }

    public void OnUpdate()
    {
        if (enemy.player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance > enemy.detectionRange * 1.5f)
        {
            enemy.SwitchToPatrol();
            return;
        }

        if (distance <= enemy.attackRange)
        {
            enemy.SwitchToAttack();
            return;
        }

        enemy.agent.SetDestination(enemy.player.position);
    }

    public void OnExit() { }
}
