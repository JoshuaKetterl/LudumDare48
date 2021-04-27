using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BossManager bossManager;
    [SerializeField] PlayerManager playerManager;
    private Animator animator;

    [FMODUnity.EventRef]
    public string StageMusicEvent = "";
    FMOD.Studio.EventInstance stageMusic;
    FMOD.Studio.PARAMETER_ID phaseParameterId;
    FMOD.Studio.PARAMETER_ID beatenParameterId;

    bool sceneActive = true;

    //TODO
    /* Player on Death Effect (despawn/respawn, heal boss 50%, back to phase 1)
     * Music Manager - Alter Music based on phase, potentially apply effect based on player health
     * VFX Manager - Alter Visual Queues based on player health
     */

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("transition").GetComponent<Animator>();

        //Initialize FMOD Objects
        stageMusic = FMODUnity.RuntimeManager.CreateInstance(StageMusicEvent);
        stageMusic.start();

        FMOD.Studio.EventDescription phaseEventDescription;
        stageMusic.getDescription(out phaseEventDescription);

        FMOD.Studio.EventDescription beatenEventDescription;
        stageMusic.getDescription(out beatenEventDescription);

        FMOD.Studio.PARAMETER_DESCRIPTION phaseParameterDescription;
        phaseEventDescription.getParameterDescriptionByName("Music Phase 2", out phaseParameterDescription);
        phaseParameterId = phaseParameterDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION beatenParameterDescription;
        beatenEventDescription.getParameterDescriptionByName("Music Boss Beaten", out beatenParameterDescription);
        beatenParameterId = beatenParameterDescription.id;
    }

    private void Update()
    {
        if (sceneActive)
        {
            //Set music parameter to proper phase
            if (bossManager.phaseTwo)
                stageMusic.setParameterByID(phaseParameterId, 1);
            else
                stageMusic.setParameterByID(phaseParameterId, 0);

            //Transition to Stinger if boss is dead
            if (bossManager.bossBeaten)
            {
                stageMusic.setParameterByID(beatenParameterId, 1);
                sceneActive = false;
                EndScene();
            }
            else
            {
                stageMusic.setParameterByID(beatenParameterId, 0);
            }
        }
    }

    private void EndScene()
    {
        animator.SetTrigger("darkLevel");
        stageMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
