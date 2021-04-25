using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommonBehavior : MonoBehaviour
{

    private void Shoot(Transform origin, float moveSpeed, float ttl)
    {
        // Get reusable bullet object from Bullet Pool
        GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();

        //Set Bullet attributes
        bullet.transform.position = origin.position;
        bullet.transform.rotation = origin.rotation;
        Bullet b = bullet.GetComponent<Bullet>();
            b.SetDirection(origin.up);
            b.SetSpeed(moveSpeed);
            b.SetHostility(true);
            b.SetTimeToLive(ttl);

        //Activate bullet object (must happen last)
        bullet.SetActive(true);
    }
}
