using UnityEngine;

public class ArrowProjectile : ProjectileBase
{
    [SerializeField] private float rotationSpeed = 720f;

    protected override void Move()
    {
        base.Move();

        // transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
