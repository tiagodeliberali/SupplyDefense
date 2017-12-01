using Assets.Utils;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectileSocket;
    [SerializeField] float shootDistance = 5f;
    [SerializeField] float hitDelay = 0.5f;

    CameraRaycaster cameraRaycaster;
    Vector3 aimOffset = new Vector3(0f, 0.5f, 0f);

    AICharacterControl ai;
    GameObject walkTarget;
    float nextHitAllowed;

    GameObject enemyTarget;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponentInParent<CameraRaycaster>();
        ai = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("walkTarget");

        cameraRaycaster.OnLeftButtonClick += ProcessMouseMovement;
    }

    private void Update()
    {
        if (enemyTarget != null)
        {
            if (Vector3.Distance(transform.position, enemyTarget.transform.position) > shootDistance)
            {
                ai.SetTarget(enemyTarget.transform);
            }
            else
            {
                ai.SetTarget(transform);
                SpawnProjectile(enemyTarget);
            }
        }
    }

    private void ProcessMouseMovement(RaycastHit raycastHit, int layer)
    {
        switch (layer)
        {
            case (int)Layer.Walkable:
                enemyTarget = null;
                walkTarget.transform.position = raycastHit.point;
                ai.SetTarget(walkTarget.transform);
                break;

            case (int)Layer.Enemy:
                GameObject enemy = raycastHit.collider.gameObject;
                enemyTarget = enemy;
                break;

            case (int)Layer.House:
                enemyTarget = null;
                break;
            default:
                print("Unexpected layer found");
                return;
        }
    }

    private void ProcessMouseAttack(RaycastHit raycastHit, int layer)
    {
        // TODO Add mellee atack/magick attack
    }

    private void SpawnProjectile(GameObject enemy)
    {
        if (Time.time < nextHitAllowed || enemy == null || projectileSocket == null)
            return;

        nextHitAllowed = Time.time + hitDelay;

        GameObject shoot = Instantiate(projectilePrefab);
        shoot.transform.position = projectileSocket.transform.position;

        Projectile projectile = shoot.GetComponent<Projectile>();
        projectile.SetOriginLayer(gameObject.layer);
        projectile.SetTarget(enemy);
    }
}
