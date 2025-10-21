using UnityEngine;

public class AttackState : IState
{
    private readonly MeleeEnemy enemy;

    public AttackState(MeleeEnemy enemy) => this.enemy = enemy;

    public void OnEnter()
    {
        enemy.agent.isStopped = true;
    }

    public void OnUpdate()
    {
        if (enemy.player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // if player moves away
        if (distance > enemy.attackRange)
        {
            enemy.SwitchToChase();
            return;
        }

        // attack cooldown
        if (Time.time >= enemy.lastAttackTime + enemy.attackCooldown)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        enemy.lastAttackTime = Time.time;
        Debug.Log($"{enemy.name} attacks player for {enemy.damage}");

        // Example: if player has Damageable
        if (enemy.player.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(int.Parse(enemy.damage.ToString()));
        }
    }

    public void OnExit()
    {
        enemy.agent.isStopped = false;
    }
}
