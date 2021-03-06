using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private float invWindow = 1f;

    [FMODUnity.EventRef] public string HealEvent = "";
    [FMODUnity.EventRef] public string TakeDamageEvent = "";

    private int currentHP;
    private bool vulnerable;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        vulnerable = true;
        animator = gameObject.GetComponentInChildren<Animator>();
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
            currentHP--;
            FMODUnity.RuntimeManager.PlayOneShot(TakeDamageEvent, transform.position);
            animator.SetTrigger("thePillDamage");
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
            }
        }
        else
        {
        }
    }

    private void Heal()
    {
        FMODUnity.RuntimeManager.PlayOneShot(HealEvent, transform.position);
        if (currentHP < maxHP)
            currentHP++;

    }

    private void OnDeath()
    {
        animator.SetTrigger("playerDeath");
        Invoke(nameof(Respawn), 1f);

    }

    private void Respawn()
    {
        currentHP = maxHP;
        transform.position = GameObject.FindGameObjectWithTag("transition").transform.position;
    }

    private void MakeVulnerable()
    {
        //End Invulnerability Window
        vulnerable = true;
    }
}
