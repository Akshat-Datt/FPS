using UnityEngine;

public class RangedAttackState : IState
{
    private readonly RangedEnemy enemy;

    public RangedAttackState(RangedEnemy enemy) => this.enemy = enemy;

    public void OnEnter()
    {
        enemy.agent.isStopped = true;
    }

    public void OnUpdate()
    {
        if (enemy.player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // If player moves away, chase again
        if (distance > enemy.attackDistance * 1.2f)
        {
            enemy.SwitchToChase();
            return;
        }

        // Look at player
        Vector3 lookPos = enemy.player.position - enemy.transform.position;
        lookPos.y = 0;
        enemy.transform.rotation = Quaternion.Slerp(
            enemy.transform.rotation,
            Quaternion.LookRotation(lookPos),
            Time.deltaTime * 5f
        );

        // Attack timer
        if (Time.time >= enemy.lastAttackTime + enemy.attackCooldown)
        {
            FireProjectile();
            enemy.lastAttackTime = Time.time;
        }
    }

    private void FireProjectile()
    {
        if (enemy.projectilePrefab == null || enemy.firePoint == null) return;

        GameObject proj = Object.Instantiate(
            enemy.projectilePrefab,
            enemy.firePoint.position,
            enemy.firePoint.rotation
        );

        if (proj.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = enemy.firePoint.forward * 15f;
        }

        // Optional — integrate pooling later
        Object.Destroy(proj, 3f);
    }

    public void OnExit()
    {
        enemy.agent.isStopped = false;
    }
}
