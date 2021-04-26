using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearBoss : BossCommonBehavior
{
    [SerializeField] private Rigidbody2D firePoint;
    [SerializeField] private GameObject pooledTentacle;
    [SerializeField] private List<Transform> tpLocations;
    [SerializeField] private List<Transform> tentacleLocations;
    
    private Animator animator;

    [FMODUnity.EventRef] public string RoarEvent = "";

    private float reloadTime = 3f;
    private readonly float screamReload = 6f;
    private readonly float screamDuration = 4f;
    private readonly float screamForce = 8f;
    private readonly bool notEnoughTentaclesInPool = true;

    private List<TentacleAttack> tentacles;
    private Vector2 targetPos;
    private Vector2 lookDirection;

    private Transform cachedFirePointTransform;
    private Transform target;
    private Rigidbody2D targetRB;

    private int tpLocationsSize;
    private int tentacleLocationsSize;
    private bool canSpawn = false;
    private bool screamAttackCD = false;
    private bool inScream = false;


    // Start is called before the first frame update
    private void Start()
    {
        tentacles = new List<TentacleAttack>();
        cachedFirePointTransform = firePoint.transform;
        animator = gameObject.GetComponentInChildren<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetRB = target.gameObject.GetComponent<Rigidbody2D>();

        tentacleLocationsSize = tentacleLocations.Count;
        tpLocationsSize = tpLocations.Count;

        Invoke(nameof(Reload), reloadTime*2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // scream attack always false in phase one
        if (!screamAttackCD)
        {
            if (canSpawn)
            {
                SpawnTentacle(target.position);
                canSpawn = false;
                Invoke(nameof(Reload), reloadTime);
            }
        }

        // when scream is off CD, always do
        else
        {
            if (!inScream)
            {
                int locationIndex = Random.Range(1, tpLocationsSize);
                transform.position = tpLocations[locationIndex].position;
                animator.SetTrigger("ScreamTrigger");

                for (int i = 0; i < tentacleLocationsSize; i++)
                {
                    SpawnTentacle(tentacleLocations[i].position);
                }
                Invoke(nameof(ScreamEnd), screamDuration);
                FMODUnity.RuntimeManager.PlayOneShot(RoarEvent, transform.position);
                inScream = true;
                print("BOSS IS SCREM");
            }
            else
            {
                targetRB.AddForce((target.position - transform.position).normalized * Time.deltaTime * screamForce, ForceMode2D.Impulse);
            }
        }
    }

    public override void PhaseOne()
    {
        base.PhaseOne();
        Invoke(nameof(tpHome), 4f);
    }

    public override void PhaseTwo()
    {
        base.PhaseTwo();
        reloadTime = 1.5f;

        Invoke(nameof(ReloadScream), screamReload);
    }

    public override void OnKill()
    {
        CancelInvoke();
        base.OnKill();
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

        if (notEnoughTentaclesInPool)
        {
            TentacleAttack t = Instantiate(pooledTentacle).GetComponent<TentacleAttack>();
            t.gameObject.SetActive(false);

            //bullet pool and manager must be passed because it cannot be set in prefab
            t.setBulletPool(bulletPool);
            t.bossManager = base.bossManager;
            tentacles.Add(t);
            return t;
        }
        return null;
    }

    public override void DealDamage(float damage)
    {
        animator.SetTrigger("damage");
        base.DealDamage(damage);
    }

    private void Reload()
    {
        canSpawn = true;
    }

    private void ReloadScream()
    {
        screamAttackCD = true;
    }

    private void ScreamEnd()
    {
        inScream = false;
        screamAttackCD = false;

        tpHome();
        Invoke(nameof(ReloadScream), screamReload);
    }

    private void tpHome()
    {
        transform.position = tpLocations[0].position;
    }
}
