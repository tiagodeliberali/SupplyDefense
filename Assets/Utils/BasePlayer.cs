﻿using UnityEngine;

namespace Assets.Utils
{
    public class BasePlayer : MonoBehaviour
    {
        public delegate void PlayerHealthEvent(float healthAsPercentage);
        public event PlayerHealthEvent OnPlayerChangesHealth;

        [SerializeField] float maxHitPoints = 100;
        [SerializeField] float currentHitPoints = 100;

        public float HealthAsPercentage
        {
            get { return currentHitPoints / maxHitPoints; }
        }

        public void LosesHitPoints(float hitPoints)
        {
            currentHitPoints -= hitPoints;

            if (OnPlayerChangesHealth != null) OnPlayerChangesHealth(HealthAsPercentage);
        }

        public void UpdatesMaxHitPoints(float maxHitPoints)
        {
            this.maxHitPoints = maxHitPoints;

            if (OnPlayerChangesHealth != null) OnPlayerChangesHealth(HealthAsPercentage);
        }
    }
}