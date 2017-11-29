using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileDamage = 10;

    private void OnTriggerEnter(Collider collider)
    {
        var damageable = collider.gameObject.GetComponentInParent<IDamageable>();

        if (damageable != null) damageable.TakeDamage(projectileDamage);
    }
}
