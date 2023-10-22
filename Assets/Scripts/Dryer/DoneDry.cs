using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoneDry : DryerState
{
    private DryerController dryerController;
    public static event Action<Laundry> OnUnloadDryer;
    public DoneDry(GameObject _dryer, Animator _anim) :
        base(_dryer, _anim)
    {
        name = STATE.DONE;
        dryerController = dryer.GetComponent<DryerController>();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (dryerController.isInteractedWith)
        {
            anim.SetTrigger("Transition");
            dryerController.isInteractedWith = false;
            OnUnloadDryer.Invoke(dryerController.loadedLaundry);
            nextState = new ReadyDry(dryer, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
