// Damageable.cs
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    private float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f) Die();
    }

    protected virtual void Die()
    {
        // for now, just disable. We'll integrate pooling later.
        gameObject.SetActive(false);
        Debug.Log($"{name} died");
    }
}
