using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private float invWindow = 1f;

    [FMODUnity.EventRef] public string TakeDamageEvent = "";
    [FMODUnity.EventRef] public string HealthPickupEvent = "";

    private int currentHP;
    private bool vulnerable;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        vulnerable = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (vulnerable && (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet")))
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            collision.gameObject.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot(HealthPickupEvent, transform.position);
            Heal();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            DealDamage();
        }
    }

    public void DealDamage()
    {
        if (vulnerable)
        {
            FMODUnity.RuntimeManager.PlayOneShot(TakeDamageEvent, transform.position);
            currentHP--;
            if (currentHP <= 0)
            {
                vulnerable = false;
                OnDeath();
            }
            else
            {
                //trigger invulnerability window
                vulnerable = false;
                Invoke(nameof(MakeVulnerable), invWindow);
                print("Player HP: " + currentHP + " / " + maxHP);
            }
        }
        else
        {
            //Debug
            print("Player is invulnerable!");
        }
    }

    private void Heal()
    {
        if (currentHP < maxHP)
            currentHP++;

        //debug
        print("Player HP: " + currentHP + " / " + maxHP);
    }

    private void OnDeath()
    {
        //Debug
        print("Player has died!");
        gameObject.SetActive(false);
    }

    private void MakeVulnerable()
    {
        //End Invulnerability Window
        vulnerable = true;
    }
}
