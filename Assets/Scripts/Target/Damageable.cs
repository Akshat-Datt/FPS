using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthBar;
    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log($"{name} destroyed!");
        // Disable or return to pool later
        EnemyPool.Instance?.ReturnEnemy(this);
    }
}
