using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;
using System;
//using PathCreation;

public class PlayerAirborne : State
{
    Player player;
    Quaternion startRotation;
    protected float trickScore;
    public PlayerAirborne(Player actor) : base(actor)
    {
        player = actor;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        startRotation = player.transform.rotation;
    }
    public override void OnExitState()
    {
        base.OnExitState();
        //player.animator.SetTrigger(AnimID.landing);
    }

    
    public override void Update()
    {
        base.Update();
        if(player.IsPlayerGrounded() || player.HasContactWithGround())
        {
            player.previousState.OnEnterState();
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
       AirFlip();
       
    }

    private void AirFlip()
    {   
        float sideMove = Input.GetAxisRaw("Horizontal") * player.flipSpeed;
        float verticalMove = Input.GetAxisRaw("Vertical") * player.flipSpeed;
        if(sideMove != 0 || verticalMove != 0)
        {
            TrickScoring(sideMove, verticalMove);
            player.playerRb.angularDrag = 10;
            
            Vector3 forward = player.transform.TransformDirection(Vector3.forward * sideMove * player.flipSpeed);
            Vector3 right = player.transform.TransformDirection(Vector3.right * verticalMove * player.flipSpeed);
            
           
            player.playerRb.AddTorque(Vector3.forward * -sideMove * player.flipSpeed, ForceMode.VelocityChange);
            player.playerRb.AddTorque(Vector3.right * verticalMove * player.flipSpeed, ForceMode.VelocityChange);
            
            
        } 
    }
    float currentScore;
    float sideFlipScore;
    float vertFlipScore;
    void TrickScoring(float sideMove, float vericalMove)
    {
        sideFlipScore += Mathf.Abs(sideMove);
        vertFlipScore += Mathf.Abs(vericalMove);
        currentScore = sideFlipScore+vertFlipScore;
        
        ScoringManager.OnScoring(currentScore);
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
