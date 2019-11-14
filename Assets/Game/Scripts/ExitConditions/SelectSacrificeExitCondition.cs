using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSacrificeExitCondition : ExitCondition
{
    private bool winStatus = false;
    
    private void OnEnable()
    {
        this.AddObserver(OnChosenSacrifice, SacrificesManager.OnChooseSacrificeNotification);
    }

    private void OnChosenSacrifice(object sender, object args)
    {
        winStatus = true;
    }

    // Update is called once per frame
    protected override bool CheckForExitCondition()
    {
        return winStatus;
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnChosenSacrifice, SacrificesManager.OnChooseSacrificeNotification);
    }
}
