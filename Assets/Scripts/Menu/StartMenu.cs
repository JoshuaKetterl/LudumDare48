using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : BossCommonBehavior
{

    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //damage 2 times cuz it has 2 health to activate phase two and then the transition
        bossManager.DealDamage(1);
        bossManager.DealDamage(1);
    }
}
