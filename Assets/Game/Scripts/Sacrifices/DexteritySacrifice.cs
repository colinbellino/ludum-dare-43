using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexteritySacrifice : SacrificeBehaviour
{
    private Shooter playerShooter;

    protected void OnEnable()
    {
        var player = GameObject.Find("Player");

        if (player)
        {
            playerShooter = player.GetComponent<Shooter>();
            playerShooter.SetCooldownMode(true);
        }
    }

//    protected void OnDisable()
//    {
//    	playerMovement.SetCooldownMode(false);
//    }
}
