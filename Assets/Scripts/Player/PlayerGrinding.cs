using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class PlayerGrinding : State
{
    Player player;
    public GrindRail grindRail;
    public ContactPoint grindContact;
    Vector3 startGrindPos;
    bool isGrinding;
    public PlayerGrinding(Player actor) : base(actor)
    {
        player = actor;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
       //player.playerRb.isKinematic = true;
    }
    public override void OnExitState()
    {
        base.OnExitState();
        //player.playerRb.isKinematic = false;
        grindRail = null;
    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(grindRail);
        if(isGrinding )
        {
            if(player.pathPosition <= 0)  return;
            player.pathPosition -= 5 * Time.fixedDeltaTime;
            //player.pathPosition = Mathf.Clamp(player.pathPosition,0,grindRail.pathCreator.path.length);
            Debug.Log(player.playerRb.velocity);
            //get the vector for the rail and set t y axis equals to the players y.
            //Vector3 railPath = grindRail.pathCreator.path.GetPointAtDistance(player.pathPosition);
            //Vector3 railingVector = new Vector3(railPath.x, player.transform.position.y, railPath.z);
            //Vector3 grindingDir = railingVector - player.transform.position;
            //player.playerRb.velocity = grindingDir.normalized * player.playerRb.velocity.magnitude;
            //player.transform.position = grindRail.pathCreator.path.GetPointAtDistance(player.pathPosition);
            //player.playerRb.MovePosition(player.playerRb.transform.position + grindingDir.normalized * Time.fixedDeltaTime);
        } 
        else
        {
           // player.previousState.OnEnterState();
        }
    }
    
   public override void OnTriggerStay(Collider col)
   {
       var rail = col.GetComponentInParent<GrindRail>();
       if(rail)
       {
           isGrinding = true;
       }
       else
       {
           isGrinding = false;
       }
   }
    bool hasContact = false;
    public override void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);
        
        if(!hasContact)
        {
            grindContact = coll.contacts[0];
            // startGrindPos = grindRail.pathCreator.path.GetClosestPointOnPath(grindContact.point);
            // player.pathPosition = grindRail.pathCreator.path.GetClosestDistanceAlongPath(startGrindPos);
            //player.playerRb.MovePosition(startGrindPos);
            // player.playerRb.drag = 2;
            // player.playerRb.angularDrag = 1;
            // Debug.Log("bo");
            hasContact = true;

        }
           
         
        
    }
 
}
