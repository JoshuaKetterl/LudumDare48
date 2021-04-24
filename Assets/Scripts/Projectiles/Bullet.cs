using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float damage = 1;
    private float moveSpeed = 3f;
    private float timeToLive = 2f;

    [SerializeField] private Rigidbody2D rb2D;

    private void OnEnable()
    {
        Invoke("Remove", timeToLive);
        rb2D.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void setSpeed(float spd)
    {
        moveSpeed = spd;
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }

    public void setTimeToLive(float ttl)
    {
        timeToLive = ttl;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //can also add some effect when bullets collide with smth
        //like an explosion for example
        //print("Bullet Collision Detected");

        if (collision.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<BossCommonBehavior>().dealDamage(damage);
        }
        Remove();

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
