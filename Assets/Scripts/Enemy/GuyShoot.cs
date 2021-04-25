using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyShoot : BossCommonBehavior
{

    [SerializeField] private Rigidbody2D firePoint;
    [SerializeField] private Rigidbody2D guyBody;
    [SerializeField] private float reloadTime;
    [SerializeField] private GameObject rickRoll;

    private Vector2 targetPos;
    private Vector2 lookDirection;

    private Transform cachedFirePointTransform;
    private Transform target;

    private bool canShoot = false;
    private float moveSpeed;

    protected override void Start()
    {
        base.Start();

        cachedFirePointTransform = firePoint.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Invoke("Reload", reloadTime * 2);
    }

    public override void PhaseOne()
    {
        base.PhaseOne();
        reloadTime = 1.5f;
        moveSpeed = .2f;
    }

    public override void PhaseTwo()
    {
        base.PhaseTwo();
        CancelInvoke();
        Reload();
        reloadTime = .2f;
        moveSpeed = 1.2f;

        Instantiate(rickRoll);
    }

    private void FixedUpdate()
    {
        targetPos = target.position;
        lookDirection = targetPos - firePoint.position;

        guyBody.MovePosition(guyBody.position + (targetPos - guyBody.position).normalized * Time.deltaTime * moveSpeed);

        if (canShoot)
        {
            if (base.InPhaseTwo())
                SpreadShot();
            else
                SingleShot();
            canShoot = false;
            Invoke("Reload", reloadTime);
        }
    
    }
    private void SingleShot()
    {
        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90f;

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle);
        base.Shoot(cachedFirePointTransform, 1.2f, 4f);
    }

    private void SpreadShot()
    {
        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90f;

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle-30f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle);
        base.Shoot(cachedFirePointTransform, 5f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle+30f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);
    }

    private void Reload()
    {
        canShoot = true;
    }
}
