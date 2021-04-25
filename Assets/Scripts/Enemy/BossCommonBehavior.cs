using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommonBehavior : MonoBehaviour
{
    private bool phaseTwo;

    protected virtual void Start()
    {
        PhaseOne();
    }

    public void Shoot(Transform origin, float moveSpeed, float ttl)
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

    public virtual void PhaseOne()
    {
        phaseTwo = false;
        print("----------Phase One Initiated!-----------");
    }

    public virtual void PhaseTwo()
    {
        phaseTwo = true;
        print("----------Phase Two Initiated!-----------");
    }

    public void OnKill()
    {
        //DEBUG
        print("-------------Boss Destroyed!--------------");
        gameObject.SetActive(false);
    }

    public bool InPhaseTwo()
    {
        return phaseTwo;
    }
}
