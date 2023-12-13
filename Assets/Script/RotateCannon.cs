using Bullet;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    [SerializeField] private float cannonRotationSpeed = 5f;
    [SerializeField] private float cannonRotationMinAngle = 0f;
    [SerializeField] private float cannonRotationMaxAngle = 180f;

    [SerializeField] private float maxTrajectoryRayDistance = 10f;
    [SerializeField] private LayerMask rayCollidingLayer;
    [SerializeField] private int maxTrajectoryRayReflections = 5;

    [SerializeField] private LineRenderer TrajectoryRay;
    private List<Vector3> trajectoryRayPositions = new List<Vector3>();

    [SerializeField] public Transform shootPosition;

    void Start()
    {
        TrajectoryRay.positionCount = maxTrajectoryRayReflections + 1;
        trajectoryRayPositions.Capacity = maxTrajectoryRayReflections + 1;
    }

    void Update()
    {
        CannonMovement();
        HandleBulletShooting();
    }

    private void CannonMovement()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, cannonRotationMinAngle, cannonRotationMaxAngle);

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90);
        transform.rotation = rotation;

        trajectoryRayPositions.Clear();
        ShootTrajectoryRay(transform.position, rotation * Vector2.up);
        TrajectoryRay.SetPositions(trajectoryRayPositions.ToArray());
    }

    private void ShootTrajectoryRay(Vector2 origin, Vector2 direction)
    {
        trajectoryRayPositions.Add(origin);

        for (int i = 0; i < maxTrajectoryRayReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxTrajectoryRayDistance, rayCollidingLayer);

            if (hit.collider != null)
            {
                origin = hit.point + hit.normal * 0.01f;
                trajectoryRayPositions.Add(origin);

                if (hit.collider.CompareTag("Wall"))
                {
                    direction = Vector2.Reflect(direction, hit.normal);
                }
                else
                {
                    origin += direction;
                    trajectoryRayPositions.Add(origin);
                    break;
                }
            }
            else
            {
                origin += direction * maxTrajectoryRayDistance;
                trajectoryRayPositions.Add(origin);
                break;
            }
        }
    }

    private void HandleBulletShooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        BulletService.Instance.GetBullet(shootPosition.position, shootPosition.rotation);
    }
}