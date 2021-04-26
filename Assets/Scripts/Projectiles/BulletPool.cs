using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private GameObject pooledBullet;

    [FMODUnity.EventRef]
    public string ShootEvent = "";

    private readonly int maxBullets = 128;
    private bool notEnoughBulletsInPool = true;

    private int bulletCount = 0;
    private List<GameObject> bullets;

    void Start()
    {
        bullets = new List<GameObject>();
    }

    public void playSound(Vector2 sourceLocation)
    {
        FMODUnity.RuntimeManager.PlayOneShot(ShootEvent, sourceLocation);
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (notEnoughBulletsInPool)
        {
            GameObject b = Instantiate(pooledBullet);
            b.SetActive(false);
            bullets.Add(b);
            if (++bulletCount > maxBullets)
                notEnoughBulletsInPool = false;
            return b;
        }
        return null;
    }
}
