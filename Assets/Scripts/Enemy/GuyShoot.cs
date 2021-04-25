using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyShoot : MonoBehaviour
{

    [SerializeField] private Rigidbody2D firePoint;
    [SerializeField] private float reloadTime = 1.5f;

    private Vector2 targetPos;
    private Vector2 lookDirection;

    private Transform cachedFirePointTransform;
    private Transform target;

    private bool canShoot = false;

    private void Start()
    {
        cachedFirePointTransform = firePoint.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Invoke("Reload", reloadTime * 2);
    }

    private void Update()
    {

        if (canShoot)
        {
            Shoot();
            canShoot = false;
            Invoke("Reload", reloadTime);
        }
    
    }

    private void Shoot()
    {
        targetPos = target.position;
        lookDirection = targetPos - firePoint.position;
        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90f;
        cachedFirePointTransform.eulerAngles = new Vector3(0, 0, angle);

        GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();
        bullet.transform.position = cachedFirePointTransform.position;
        bullet.transform.rotation = cachedFirePointTransform.rotation;
        Bullet b = bullet.GetComponent<Bullet>();
            b.SetDirection(cachedFirePointTransform.up);
            b.SetSpeed(1.5f);
            b.SetHostility(true);
            b.SetTimeToLive(2);

        //Activate bullet object (must happen last)
        bullet.SetActive(true);
    }

    private void Reload()
    {
        canShoot = true;
    }
}
