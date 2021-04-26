using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAttack : BossCommonBehavior
{
    [SerializeField] private Rigidbody2D firePoint;

    private Transform cachedFirePointTransform;

    private void Start()
    {
        cachedFirePointTransform = firePoint.transform;
    }

    private void OnEnable()
    {
        //Change this to be part of animation
        Invoke(nameof(SpreadShot), 3f);
        Invoke(nameof(Remove), 4f);
    }

    public void setBulletPool(BulletPool bp)
    {
        base.bulletPool = bp;
    }

    private void SpreadShot()
    {
        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 0f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 30f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 60f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 90f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 120f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 150f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 180f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 210f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 240f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 270f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 300f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 330f);
        base.Shoot(cachedFirePointTransform, 4f, 4f);
    }

    private void Remove()
    {
        gameObject.SetActive(false);
    }
}
