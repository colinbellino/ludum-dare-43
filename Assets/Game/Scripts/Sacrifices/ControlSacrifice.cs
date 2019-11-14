using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSacrifice : SacrificeBehaviour
{
    private Movement playerMovement;

    protected void OnEnable()
    {
        var player = GameObject.Find("Player");

        if (player)
        {
            playerMovement = player.GetComponent<Movement>();
            playerMovement.SetIceMode(true);
        }
    }

//    protected void OnDisable()
//    {
//    	playerMovement.SetIceMode(false);
//    }
}
