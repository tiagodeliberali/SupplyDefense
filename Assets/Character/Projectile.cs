using Assets.Utils;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    [SerializeField] float speed = 0.1f;
    [SerializeField] Vector3 aim = new Vector3(0f, 0.8f, 0f);

    private int originLayer;
    private Rigidbody rb;
    private GameObject target;

    public void SetOriginLayer(int layer)
    {
        originLayer = layer;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target != null)
        {
            rb.velocity = (target.transform.position + aim - transform.position) * speed;
        }
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
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
