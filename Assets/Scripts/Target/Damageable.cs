using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    [Tooltip("Assign a UI Image with Fill Mode set to 'Filled'.")]
    [SerializeField] private Image healthFillImage;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log($"{name} destroyed!");
        // Return to pool if it's an enemy, otherwise disable
        if (EnemyPool.Instance != null)
            EnemyPool.Instance.ReturnEnemy(this);
        else
            gameObject.SetActive(false);
    }
}
