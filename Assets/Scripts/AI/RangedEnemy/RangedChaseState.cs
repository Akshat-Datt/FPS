using UnityEngine;

public class RangedChaseState : IState
{
    private readonly EnemyBase enemy;

    public RangedChaseState(EnemyBase enemy) => this.enemy = enemy;

    public void OnEnter() { }

    public void OnUpdate()
    {
        if (enemy.player == null) return;

        enemy.agent.SetDestination(enemy.player.position);

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (enemy is MeleeEnemy melee)
        {
            if (distance <= melee.attackRange)
                melee.SwitchToAttack();
        }
        else if (enemy is RangedEnemy ranged)
        {
            if (distance <= ranged.attackDistance)
                ranged.SwitchToAttack();
        }
    }

    public void OnExit() { }
}
