using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommonBehavior : MonoBehaviour
{
    [SerializeField] public BulletPool bulletPool;
    [SerializeField] public BossManager bossManager;

    public virtual void DealDamage(float damage)
    {
        //Default behavior is to damage boss health
        //Override for enemies with health pools independent from boss
        bossManager.DealDamage(damage);
    }

    protected void Shoot(Transform origin, float moveSpeed, float ttl)
    {
        // Get reusable bullet object from Bullet Pool
        GameObject bullet = bulletPool.GetBullet();

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

    public virtual void PhaseOne()
    {
        print("----------Phase One Initiated!-----------");
    }

    public virtual void PhaseTwo()
    {
        print("----------Phase Two Initiated!-----------");
    }

    public virtual void OnKill()
    {
        //DEBUG
        print("-------------Boss Destroyed!--------------");
        gameObject.SetActive(false);
    }
}
