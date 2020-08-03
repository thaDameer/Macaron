using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class PlayerIdle : State
{
    Player player;
    public PlayerIdle(Player actor) : base(actor)
    {
        player = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        player.playerRb.isKinematic = true;
    }
    public override void OnExitState()
    {
        base.OnExitState();
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetMouseButtonDown(0) && player.IsPointingAtPlayer())
        {
            player.sPlayerAiming.OnEnterState();
            //player.ActivateAimingArrow(true);        
            
        } 
       
    }

}
