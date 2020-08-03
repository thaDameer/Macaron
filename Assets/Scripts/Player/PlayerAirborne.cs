using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;
//using PathCreation;

public class PlayerAirborne : State
{
    Player player;
    public PlayerAirborne(Player actor) : base(actor)
    {
        player = actor;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
    }
    public override void OnExitState()
    {
        base.OnExitState();
        //player.animator.SetTrigger(AnimID.landing);
    }

    float flipSpeed = 5;
    public override void Update()
    {
        base.Update();
        if(player.IsPlayerGrounded())
        {
            player.previousState.OnEnterState();
        }
        float sideMove = Input.GetAxisRaw("Horizontal") * flipSpeed;
        float verticalMove = Input.GetAxisRaw("Vertical") * flipSpeed;
        if(sideMove != 0 || verticalMove != 0)
        {
            player.transform.Rotate(player.transform.forward * -sideMove ,Space.World);
            player.transform.Rotate(player.transform.right * verticalMove, Space.World);
        }
       
    }
    public override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);  
    }
    public override void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);
    }
    
    public override void OnCollisionStay(Collision other) 
    {
        base.OnCollisionStay(other);
       
    }
}
