using Assets.Utils;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileDamage = 10;

    private int originLayer;

    public void SetOriginLayer(int layer)
    {
        originLayer = layer;
    }

    private void OnTriggerEnter(Collider collider)
    {
        int hitLayer = collider.gameObject.layer;

        if (hitLayer == (int)Layer.Walkable || hitLayer == (int)Layer.House)
        {
            Destroy(gameObject);
        }

        var damageable = collider.gameObject.GetComponentInParent<IDamageable>();

        if (damageable != null && hitLayer != originLayer)
        {
            damageable.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
