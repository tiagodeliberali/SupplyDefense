using UnityEngine;

namespace Assets.Utils
{
    public class BasePlayer : MonoBehaviour, IDamageable
    {
        public delegate void PlayerHealthEvent(float healthAsPercentage);
        public event PlayerHealthEvent OnPlayerChangesHealth;

        [SerializeField] float maxHealthPoints = 100;
        [SerializeField] float currentHealthPoints = 100;

        public float HealthAsPercentage
        {
            get { return currentHealthPoints / maxHealthPoints; }
        }

        public void TakeDamage(float damage, int layer)
        {
            if (gameObject.layer == layer) // avoid self inflicting damage
                return;

            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);

            if (OnPlayerChangesHealth != null) OnPlayerChangesHealth(HealthAsPercentage);
        }

        public void UpdatesMaxHitPoints(float maxHitPoints)
        {
            this.maxHealthPoints = maxHitPoints;

            if (OnPlayerChangesHealth != null) OnPlayerChangesHealth(HealthAsPercentage);
        }
    }
}
