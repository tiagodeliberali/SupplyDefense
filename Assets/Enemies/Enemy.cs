using Assets.Utils;
using System;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.Enemies
{
    public class Enemy : BasePlayer
    {
        [SerializeField] float attackDistance = 4f;
        [SerializeField] float meleeDamage = 2f;

        GameObject player;
        GameObject house;
        AICharacterControl ai;
        Rigidbody rb;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            house = SelectHouse();
            ai = GetComponentInChildren<AICharacterControl>();
            rb = GetComponentInChildren<Rigidbody>();
        }

        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, rb.transform.position);

            if (house == null)
            {
                house = SelectHouse();
            }

            if (distanceToPlayer <= attackDistance)
            {
                ai.SetTarget(player.transform);
            }
            else
            {
                ai.SetTarget(house.transform);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            int hitLayer = collider.gameObject.layer;

            var damageable = collider.gameObject.GetComponentInParent<IDamageable>();

            if (damageable != null && hitLayer == (int)Layer.Player)
            {
                damageable.TakeDamage(meleeDamage);
            }
        }

        private GameObject SelectHouse()
        {
            return GameObject.FindGameObjectsWithTag("House").FirstOrDefault();
        }
    }
}
