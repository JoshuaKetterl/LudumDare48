using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearBoss : BossCommonBehavior
{
    [SerializeField] private Rigidbody2D firePoint;
    [SerializeField] private Rigidbody2D mainBody;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private GameObject pooledTentacle;
    private List<TentacleAttack> tentacles;
    private Vector2 targetPos;
    private Vector2 lookDirection;

    private Transform cachedFirePointTransform;
    private Transform target;

    private bool canShoot = false;
    private bool notEnoughtentaclesInPool = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        tentacles = new List<TentacleAttack>();
        cachedFirePointTransform = firePoint.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Invoke(nameof(Reload), 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canShoot)
        {
            if (base.InPhaseTwo())
                TargetedTentacle();
            else
                TargetedTentacle();
            canShoot = false;
            Invoke(nameof(Reload), reloadTime);
        }
    }

    //DEBUG
    private void TargetedTentacle()
    {
        SpawnTentacle(target.position + new Vector3(0f, 0f, 0f));
    }

    public void SpawnTentacle(Vector2 target)
    {
        TentacleAttack tentacle = GetTentacle();
        tentacle.transform.position = target;
        tentacle.gameObject.SetActive(true);
    }

    public TentacleAttack GetTentacle()
    {
        if (tentacles.Count > 0)
        {
            for (int i = 0; i < tentacles.Count; i++)
            {
                if (!tentacles[i].gameObject.activeInHierarchy)
                {
                    return tentacles[i];
                }
            }
        }

        if (notEnoughtentaclesInPool)
        {
            TentacleAttack t = Instantiate(pooledTentacle).GetComponent<TentacleAttack>();
            t.gameObject.SetActive(false);

            //bullet pool must be passed because it cannot be set in prefab
            t.setBulletPool(bulletPool);
            tentacles.Add(t);
            return t;
        }
        return null;
    }

    private void Reload()
    {
        canShoot = true;
    }
}
