using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager bossManagerInstance;

    [SerializeField] private float maxHP = 20;
    private float currentHP;
    private float phaseTwoHP;

    private bool phaseTwo = false;
    private bool vulnerable;

    // Start is called before the first frame update
    void Start()
    {
        vulnerable = true;
        currentHP = maxHP;
        phaseTwoHP = maxHP / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHP()
    {
        return currentHP;
    }

    public void DealDamage(float dmg)
    {
        if (vulnerable)
        {
            currentHP -= dmg;

            if (!phaseTwo && currentHP <= phaseTwoHP)
            {
                phaseTwo = true;
                OnPhaseTwo();
            }
            else if (currentHP <= 0)
            {
                vulnerable = false;
                OnKill();
            }
            else
            {
                //DEBUG
                print("Boss HP: " + currentHP + " / " + maxHP);
            }
        }
        else
        {
            //DEBUG
            print("Boss is immune to damage!");
        }
    }

    private void OnPhaseTwo()
    {
        //DEBUG
        print("----------Phase Two Initiated!-----------");
    }

    private void OnKill()
    {
        //DEBUG
        print("-------------Boss Destroyed!--------------");
        gameObject.SetActive(false);
    }
}
