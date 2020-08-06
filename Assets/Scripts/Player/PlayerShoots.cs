using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class PlayerShoots : State
{
    Player player;
    float delayTimer;
    public PlayerShoots(Player actor) : base(actor)
    {
        player = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        player.playerRb.isKinematic = false;
        player.playerRb.angularDrag = 0.05f;
        
        //player.playerRb.AddForce(player.movingDirection * (player.shootSpeed*player.multiplierSpeed),ForceMode.Impulse);
        if(player.movingDirection != Vector3.zero)
        {
            player.playerRb.velocity = player.movingDirection * player.shootSpeed * player.multiplierSpeed;
        }
        //timer used to delay the player control at start of state entering
        delayTimer=0;
        //reset the moving direction to zero after the first shootout
        player.movingDirection = Vector3.zero;
        
    }
    public override void OnExitState()
    {
        base.OnExitState();
    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        float xMovement = player.movementInput.x != 0 ? player.movementInput.x * player.movementSpeed: 0;
        
        //Only able to control if vMagnitude is above 14 
        if(!player.IsPlayerGrounded())return; 
        if(player.playerRb.velocity.magnitude > 14)
        {
            Vector3 movingDir = new Vector3(xMovement,player.playerRb.velocity.y,player.playerRb.velocity.z);
            //player.playerRb.velocity = movingDir;
            player.playerRb.AddForce(new Vector3(player.movementInput.x,0,0) * player.movementSpeed,ForceMode.Impulse); 
        }
        
    }

    public override void Update()
    {
        base.Update();
        delayTimer+=Time.deltaTime;
        
        if(delayTimer > 1)
        {
            if(player.playerRb.velocity.magnitude < 2 && !GameManager.instance.levelHandler.isLevelFinished)
            {
                player.sPlayerDead.OnEnterState();
            }
        }
        if(player.HasContactWithGround())
        {
            return;
        } else if(!player.IsPlayerGrounded())
        {
            player.sPlayerAirborne.OnEnterState();
        }

        
    }
    bool isGrinding = false;
    Vector3 grindPartyPos;
    
    public override void OnCollisionEnter(Collision coll)
    {   
        //test for grinding mechanic
        // if(isGrinding)
        // {
        //     ContactPoint contact = coll.contacts[0];
        //     player.ps.transform.position = contact.point;
        // }
        base.OnCollisionEnter(coll);
        
    }
   
    public override void OnTriggerStay(Collider col)
    {
        base.OnTriggerStay(col);
        // if(!isGrinding)
        // {
        //     isGrinding = true;
        //     grindPartyPos = col.transform.position;
           
        // }
        //player.playerRb.angularDrag = 8f;

        // GrindRail grindRail = col.GetComponentInParent<GrindRail>();
        // if(grindRail)
        // {
        //     player.ps.Emit(10);
        // }

    }
    public override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        // isGrinding = false;
        // player.playerRb.angularDrag= 0.05f;
        // var emissionModule = player.ps.emission;
        // emissionModule.rateOverDistance = 0;
    }
}
