using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private float invWindow = 2f;

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
        if (vulnerable && collision.tag.Equals("Enemy"))
        {
            DealDamage();
        }
    }

    public void DealDamage()
    {
        if (vulnerable)
        {
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
                Invoke("makeVulnerable", invWindow);
                print("Player HP: " + currentHP + " / " + maxHP);
            }
        }
        else
        {
            //Debug
            print("Player is invulnerable!");
        }
    }

    public void Heal()
    {
        if (currentHP < maxHP)
            currentHP++;

        //debug
        print("Player HP: " + currentHP + " / " + maxHP);
    }

    public void OnDeath()
    {
        //Debug
        print("Player has died!");
        gameObject.SetActive(false);
    }

    private void makeVulnerable()
    {
        vulnerable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
