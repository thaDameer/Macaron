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

    
    public override void Update()
    {
        base.Update();
        if(player.IsPlayerGrounded())
        {
            player.previousState.OnEnterState();
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
       AirFlip();
       Debug.Log(player.playerRb.angularVelocity);
    }

    private void AirFlip()
    {   
        float sideMove = Input.GetAxisRaw("Horizontal") * player.flipSpeed;
        float verticalMove = Input.GetAxisRaw("Vertical") * player.flipSpeed;
        if(sideMove != 0 || verticalMove != 0)
        {
          
            var eulerAngleVelocity = new Vector3(verticalMove,0, -sideMove);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
            player.playerRb.MoveRotation(player.playerRb.rotation * deltaRotation);

            //player.playerRb.angularVelocity = new Vector3 (verticalMove, player.playerRb.angularVelocity.y, -sideMove);
            // player.transform.Rotate(player.transform.forward * -sideMove ,Space.World);
            // player.transform.Rotate(player.transform.right * verticalMove, Space.World);
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
