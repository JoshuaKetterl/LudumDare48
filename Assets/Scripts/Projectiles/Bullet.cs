using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private bool hostile;
    private float damage;
    private float moveSpeed;
    private float timeToLive = 2f;

    [SerializeField] private Rigidbody2D rb2D;

    private void OnEnable()
    {
        Invoke(nameof(Remove), timeToLive);
        rb2D.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void SetSpeed(float spd)
    {
        moveSpeed = spd;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetTimeToLive(float ttl)
    {
        timeToLive = ttl;
    }

    public void SetHostility(bool h)
    {
        hostile = h;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hostile && collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<BossCommonBehavior>().DealDamage(damage);
            Remove();
        }
        else if (!hostile && (collision.CompareTag("levelCollider") || collision.CompareTag("cathedralLevelCollider")))
        {
            Remove();
        }
        else if (hostile && collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().DealDamage();
        }
        else if (hostile && collision.CompareTag("cathedralLevelCollider"))
        {
            Remove();
        }
    }

    private void Remove()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
