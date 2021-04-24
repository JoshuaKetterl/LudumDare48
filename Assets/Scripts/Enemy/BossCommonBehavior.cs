using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommonBehavior : MonoBehaviour
{
    public float hitpointsMax;
    public float hitpointsCur;

    private float phaseTwoHP;
    private bool phaseTwo = false;

    // Start is called before the first frame update
    void Start()
    {
        hitpointsMax = 20;
        hitpointsCur = hitpointsMax;
        phaseTwoHP = hitpointsMax / 2;
    }

    public float getHP()
    {
        return hitpointsCur;
    }

    public void dealDamage(float dmg)
    {
        hitpointsCur -= dmg;

        if (!phaseTwo && hitpointsCur <= phaseTwoHP)
        {
            phaseTwo = true;
            onPhaseTwo();
        }
        if (hitpointsCur <= 0)
            onKill();
        else
            //DEBUG
            print("Remaining HP: " + hitpointsCur + " / " + hitpointsMax);
    }

    private void onPhaseTwo()
    {
        //DEBUG
        print("----------Phase Two Initiated!-----------");
    }

    private void onKill()
    {
        //DEBUG
        print("-------------Boss Destroyed!--------------");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
