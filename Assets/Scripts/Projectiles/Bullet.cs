using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
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

    public void setTimeToLive(float ttl)
    {
        timeToLive = ttl;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //can also add some effect when bullets collide with smth
        //like an explosion for example
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
