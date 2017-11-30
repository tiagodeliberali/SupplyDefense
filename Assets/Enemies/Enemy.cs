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

        System.Random random;

        GameObject player;
        GameObject house;
        AICharacterControl ai;
        Rigidbody rb;

        private void Start()
        {
            random = new System.Random();

            player = GameObject.FindGameObjectWithTag("Player");
            house = SelectHouse();
            ai = GetComponentInChildren<AICharacterControl>();
            rb = GetComponentInChildren<Rigidbody>();
        }

        private void Update()
        {
            if (player == null)
            {
                ai.SetTarget(transform);
                return;
            }

            float distanceToPlayer = Vector3.Distance(player.transform.position, rb.transform.position);

            if (house == null)
            {
                house = SelectHouse();
            }

            if (distanceToPlayer <= attackDistance)
            {
                ai.SetTarget(player.transform);
            }
            else if (house != null)
            {
                ai.SetTarget(house.transform);
            }
            else
            {
                ai.SetTarget(player.transform);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            int hitLayer = collider.gameObject.layer;

            var damageable = collider.gameObject.GetComponentInParent<IDamageable>();

            if (damageable != null && 
                (hitLayer == (int)Layer.Player || hitLayer == (int)Layer.House))
            {
                damageable.TakeDamage(meleeDamage);
            }
        }

        private GameObject SelectHouse()
        {
            var houses = GameObject.FindGameObjectsWithTag("House");

            if (houses.Length > 0)
                return houses[random.Next(0, houses.Length - 1)];

            return null;
        }
    }
}
