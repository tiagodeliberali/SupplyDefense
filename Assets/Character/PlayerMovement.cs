using System;
using Assets.Utils;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectileSocket;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    AICharacterControl ai;
    GameObject walkTarget;

    bool isInDirectMode = false;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponentInParent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        ai = GetComponent<AICharacterControl>();
        currentDestination = transform.position;
        walkTarget = new GameObject("walkTarget");

        cameraRaycaster.OnLayerClick += ProcessMouseMovement;
    }

    private void ProcessMouseMovement(RaycastHit raycastHit, int layer)
    {
        clickPoint = raycastHit.point;
        switch (layer)
        {
            case (int)Layer.Walkable:
                walkTarget.transform.position = raycastHit.point;
                ai.SetTarget(walkTarget.transform);
                break;

            case (int)Layer.Enemy:
                GameObject enemy = raycastHit.collider.gameObject;
                ai.SetTarget(enemy.transform);

                SpawnProjectile(enemy.transform.position);
                break;

            case (int)Layer.House:
                
                break;
            default:
                print("Unexpected layer found");
                return;
        }
    }

    private void SpawnProjectile(Vector3 enemyPosition)
    {
        GameObject shoot = Instantiate(projectilePrefab);
        shoot.transform.position = projectileSocket.transform.position;

        Projectile projectile = shoot.GetComponent<Projectile>();
        projectile.SetOriginLayer(gameObject.layer);

        Rigidbody rb = shoot.GetComponent<Rigidbody>();

        rb.velocity = (enemyPosition - projectileSocket.transform.position) * 4f;
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
