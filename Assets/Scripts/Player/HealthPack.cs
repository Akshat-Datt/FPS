using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthPack : MonoBehaviour
{
    [Header("Health Pack Settings")]
    [SerializeField] private int healAmount = 25;
    [SerializeField] private bool respawnable = false;
    [SerializeField] private float respawnTime = 10f;

    [Header("Visuals & Audio (Optional)")]
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private AudioClip pickupSound;

    private Collider col;
    private Renderer rend;
    private AudioSource audioSource;

    private void Awake()
    {
        col = GetComponent<Collider>();
        rend = GetComponentInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();

        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only player should collect
        if (!other.CompareTag("Player")) return;

        // Check if player has a Damageable component
        Damageable playerHealth = other.GetComponent<Damageable>();
        if (playerHealth == null) return;

        // Heal the player
        playerHealth.Heal(healAmount);
        Debug.Log($"Player healed by {healAmount}");

        // Play feedback (visual + audio)
        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        if (pickupSound != null && audioSource != null)
            audioSource.PlayOneShot(pickupSound);

        // Handle pickup behavior
        if (respawnable)
            StartCoroutine(RespawnRoutine());
        else
            gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator RespawnRoutine()
    {
        col.enabled = false;
        if (rend != null) rend.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        col.enabled = true;
        if (rend != null) rend.enabled = true;
    }
}
