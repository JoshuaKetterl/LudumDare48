using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager bossManagerInstance;
    public BossCommonBehavior bossInstance;

    [SerializeField] private float maxHP = 50;
    private float currentHP;
    private float phaseTwoHP;

    private bool vulnerable;

    // Start is called before the first frame update
    void Start()
    {
        vulnerable = true;
        currentHP = maxHP;
        phaseTwoHP = maxHP / 2;
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

            if (!bossInstance.InPhaseTwo() && currentHP <= phaseTwoHP)
            {
                bossInstance.PhaseTwo();
            }
            else if (currentHP <= 0)
            {
                vulnerable = false;
                bossInstance.OnKill();
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
