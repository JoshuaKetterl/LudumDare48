using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiltBoss : BossCommonBehavior
{
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform rayPointTransform;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Gradient redColor;
    [SerializeField] private Gradient greenColor;

    [SerializeField] private List<Transform> locations;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform sourceLocation;

    [SerializeField] private float timerDefault = 2.5f;

    private float timer;

    Collider2D[] overlappingColliders = new Collider2D[1];
    ContactFilter2D contactFilter2D = new ContactFilter2D().NoFilter();

    private int locationsListSize;

    [SerializeField] private Rigidbody2D firePoint;
    [SerializeField] private float reloadTime;

    private Vector2 targetPos;
    private Vector2 lookDirection;

    private Transform cachedFirePointTransform;
    private Transform cachedBossTransform;
    private Transform target;

    private bool canShoot = false;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;

        cachedBossTransform = transform;
        locationsListSize = locations.Count;

        timer = timerDefault;

        cachedFirePointTransform = firePoint.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Invoke(nameof(Reload), reloadTime);
    }

    public void FixedUpdate()
    {
        targetPos = target.position;
        lookDirection = targetPos - firePoint.position;
        DetectPlayer();

        if (canShoot)
        {
            if (base.bossManager.phaseTwo)
                SpreadShot();
            else
                SingleShot(cachedFirePointTransform, 4f, 4f);
            canShoot = false;
            Invoke(nameof(Reload), reloadTime);
        }
    }

    private void DetectPlayer()
    {
        RaycastHit2D bossLineOfSight = Physics2D.Raycast(rayPointTransform.position, targetPos - (Vector2)rayPointTransform.position, rayDistance);

        if (bossLineOfSight.collider != null && bossLineOfSight.collider.transform.parent != null)
        {
            //Debug.DrawLine(rayPointTransform.position, bossLineOfSight.point, Color.red);
            lineRenderer.SetPosition(1, bossLineOfSight.point);
            lineRenderer.colorGradient = redColor;

            if (bossLineOfSight.collider.transform.parent.CompareTag("Player"))
            {
                //Debug.DrawLine(rayPointTransform.position, bossLineOfSight.point, Color.green);
                lineRenderer.SetPosition(1, bossLineOfSight.point);
                lineRenderer.colorGradient = greenColor;

                timer = timerDefault;
                if (canShoot)
                {
                    SpreadShot();
                    canShoot = false;
                    Invoke(nameof(Reload), reloadTime);
                }
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    Teleport();
            }
        }

        lineRenderer.SetPosition(0, rayPointTransform.position);
    }

    private void Teleport()
    {
        int locationIndex = Random.Range(0, locationsListSize);

        int numberColliders = locations[locationIndex].GetComponent<Collider2D>().OverlapCollider(contactFilter2D, overlappingColliders);

        if (numberColliders == 0)
        {
            cachedBossTransform.position = locations[locationIndex].position;
        }
        else if (numberColliders != 0 && (locationIndex + 1) >= locationsListSize)
        {
            locationIndex--;
            cachedBossTransform.position = locations[locationIndex].position;
        }
        else if (numberColliders != 0 && (locationIndex - 1) <= -1)
        {
            locationIndex++;
            cachedBossTransform.position = locations[locationIndex].position;
        }

        if (locationIndex == 1 || locationIndex == 3)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        timer = timerDefault;
    }

    public override void PhaseOne()
    {
        base.PhaseOne();
        reloadTime = 0.8f;
        timerDefault = 2.5f;
    }

    public override void PhaseTwo()
    {
        base.PhaseTwo();
        CancelInvoke();
        Reload();
        reloadTime = 0.68f;
        timerDefault = 1.5f;
    }

    private void SingleShot(Transform origin, float moveSpeed, float ttl)
    {
        bulletPool.PlaySound(sourceLocation.position);

        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90f;

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle);
        base.Shoot(cachedFirePointTransform, 4f, 4f);
    }

    private void SpreadShot()
    {
        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90f;

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle - 30f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle - 15f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle);
        SingleShot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle + 15f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle + 30f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);
    }

    private void Reload()
    {
        canShoot = true;
    }
}
