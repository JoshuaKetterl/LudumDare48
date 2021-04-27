using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAttack : BossCommonBehavior
{
    [SerializeField] private Rigidbody2D firePoint;
    [FMODUnity.EventRef] public string SurfaceEvent = "";

    private Transform cachedFirePointTransform;
    private float ttl = 3f;
    private float bulletSpeed = 1.5f;

    private void Start()
    {
        cachedFirePointTransform = firePoint.transform;
    }

    private void OnEnable()
    {
        //Change this to be part of animation
        Invoke(nameof(PlaySound), 0.05f);
        Invoke(nameof(SpreadShot), 3.5f);
        Invoke(nameof(Remove), 3.5f);
    }

    public void setBulletPool(BulletPool bp)
    {
        base.bulletPool = bp;
    }

    private void SpreadShot()
    {
        bulletPool.PlaySound(transform.position);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 0f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 30f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 60f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 90f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 120f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 150f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 180f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 210f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 240f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 270f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 300f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);

        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, 330f);
        base.Shoot(cachedFirePointTransform, bulletSpeed, ttl);
    }

    private void Remove()
    {
        gameObject.SetActive(false);
    }

    private void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SurfaceEvent, transform.position);
    }
}
