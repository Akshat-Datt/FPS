using UnityEngine;

public class ArrowProjectile : ProjectileBase
{
    [SerializeField] private float rotationSpeed = 720f; // for spin or alignment

    protected override void Move()
    {
        // Move forward as normal
        base.Move();

        // Optional: orient towards velocity (if Rigidbody-based)
        // transform.forward = rb.velocity.normalized;

        // Add a spin effect
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // Could add arrow-specific impact FX or sound here
    }
}
