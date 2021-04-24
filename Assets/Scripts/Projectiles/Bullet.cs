using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //can also add some effect when bullets collide with smth
        //like an explosion for example
        Destroy(gameObject);
    }
}
