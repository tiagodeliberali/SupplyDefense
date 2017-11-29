using Assets.Utils;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    bool isInDirectMode = false;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponentInParent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;

        cameraRaycaster.OnLayerClick += ProcessMouseMovement;
    }

    private void ProcessMouseMovement(RaycastHit raycastHit, int layer)
    {
        clickPoint = raycastHit.point;
        switch (layer)
        {
            case (int)Layer.Walkable:
                currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                break;
            case (int)Layer.Enemy:
                currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
                break;
            default:
                print("Unexpected layer found");
                return;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) // G for gamepad
        {
            isInDirectMode = !isInDirectMode;
            currentDestination = transform.position;
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            WalkToDestination();
        }
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

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= 0.5f)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        // Draw movement gizmos
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination, 0.15f);
        Gizmos.DrawSphere(clickPoint, 0.1f);

        // Draw attack sphere
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}
