using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D firePoint;

    private Vector2 targetPos;
    private Vector2 lookDirection;

    private Transform cachedFirePointTransform;
    private Transform target;

    private void Start()
    {
        cachedFirePointTransform = firePoint.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {

        if (Input.GetButtonDown("Fire1"))
            Shoot();
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
        bullet.GetComponent<Bullet>().SetDirection(cachedFirePointTransform.up);
        bullet.SetActive(true);
    }
}
