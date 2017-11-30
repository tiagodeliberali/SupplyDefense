﻿using Assets.Utils;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectileSocket;
    [SerializeField] float hitDelay = 0.5f;

    ThirdPersonCharacter thirdPersonCharacter;
    CameraRaycaster cameraRaycaster;
    Vector3 aimOffset = new Vector3(0f, 0.5f, 0f);

    AICharacterControl ai;
    GameObject walkTarget;
    float nextHitAllowed;

    bool isInDirectMode = false;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponentInParent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        ai = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("walkTarget");

        cameraRaycaster.OnRigthButtonClick += ProcessMouseMovement;
        cameraRaycaster.OnLeftButtonClick += ProcessMouseAttack;
    }

    private void ProcessMouseMovement(RaycastHit raycastHit, int layer)
    {
        switch (layer)
        {
            case (int)Layer.Walkable:
                walkTarget.transform.position = raycastHit.point;
                ai.SetTarget(walkTarget.transform);
                break;

            case (int)Layer.Enemy:
                GameObject enemy = raycastHit.collider.gameObject;
                ai.SetTarget(enemy.transform);
                break;

            case (int)Layer.House:
                
                break;
            default:
                print("Unexpected layer found");
                return;
        }
    }

    private void ProcessMouseAttack(RaycastHit raycastHit, int layer)
    {
        SpawnProjectile(raycastHit.point);
    }

    private void SpawnProjectile(Vector3 enemyPosition)
    {
        if (Time.time < nextHitAllowed)
            return;

        nextHitAllowed = Time.time + hitDelay;

        GameObject shoot = Instantiate(projectilePrefab);
        shoot.transform.position = projectileSocket.transform.position;

        Projectile projectile = shoot.GetComponent<Projectile>();
        projectile.SetOriginLayer(gameObject.layer);

        Rigidbody rb = shoot.GetComponent<Rigidbody>();

        rb.velocity = (enemyPosition + aimOffset - projectileSocket.transform.position) * 4f;
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }
}
