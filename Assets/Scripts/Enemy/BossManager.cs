using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    //Handles the Primary Boss Instance and tracks the HP amount which triggers Phase transistions.
    // Must be configured as serialized field with GameManager for handling phase change FX

    [SerializeField] public BossCommonBehavior primaryBossInstance;
    [SerializeField] public float maxHP;

    public bool phaseTwo = false;

    private BossCommonBehavior bossInstance;
    private float currentHP;
    private float phaseTwoHP;

    private bool vulnerable;

    // Start is called before the first frame update
    void Start()
    {
        vulnerable = true;
        currentHP = maxHP;
        phaseTwoHP = maxHP / 2;

        primaryBossInstance.PhaseOne();
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
                primaryBossInstance.PhaseTwo();
            }
            else if (currentHP <= 0)
            {
                vulnerable = false;
                primaryBossInstance.OnKill();
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

}
