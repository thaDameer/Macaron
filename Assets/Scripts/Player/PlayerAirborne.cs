using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;
using System;
using DG.Tweening;
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
        //turns true when player has scored enough points, resets every entry
        canGainTrickBoost = false;
        ResetCurrentScore();
    }
    public override void OnExitState()
    {
        
        base.OnExitState();
        //player.animator.SetTrigger(AnimID.landing);
       
    }

    
    public override void Update()
    {
        base.Update();
        //
        canGainTrickBoost = currentScore >= 800 ? true : false;
        if(player.IsPlayerGrounded() || player.HasContactWithGround())
        {
            if(canGainTrickBoost)
            {
                return;
            }
            ScoringManager.OnHideScore();
            player.previousState.OnEnterState();
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AirFlip();
        if(player.IsPlayerGrounded() && canGainTrickBoost || player.HasContactWithGround() && canGainTrickBoost)
        {
            CheckForPerfectLanding();
        }
    }

    private void AirFlip()
    {   
        float sideMove = Input.GetAxisRaw("Horizontal") * player.flipSpeed;
        float verticalMove = Input.GetAxisRaw("Vertical") * player.flipSpeed;
        if(sideMove != 0 || verticalMove != 0)
        {
            //Used to calculate the scoring when doing tricks in air
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

    void ResetCurrentScore()
    {
        sideFlipScore = 0;
        vertFlipScore = 0;
        currentScore = 0;
    }
    //can only perfect land if the player have made some cool tricks
    bool canGainTrickBoost;
    Func<float,float,float> OnGainingScore;
    void TrickScoring(float side,float vert)
    {
        //might be obsolete, but saving for learnig more about lambda expressions
        OnGainingScore = (s,v) => 
        {
            sideFlipScore += Mathf.Abs(s);
            vertFlipScore += Mathf.Abs(v);
            currentScore = sideFlipScore+vertFlipScore;
            return currentScore;
        };
        //Update the current score in realtime
        ScoringManager.OnScoring(OnGainingScore(side,vert));
    }
    Timer perfectLanding = new Timer(0.3f);
    
    bool succesfulLanding;
    public void CheckForPerfectLanding()
   {
      RaycastHit hit;
    
      if(Physics.Raycast(player.transform.position,-player.transform.up,out hit,3f,player.groundLayer))
      {
            //calculate the dot product of the hit normal and the players local up direction
            float cosine = Vector3.Dot(player.transform.up,hit.normal);
            float cosineDegrees = Mathf.Acos(cosine);
        
            //hard coded value of minumum angle difference
            if(cosine >= 0.85f)
            {
                perfectLanding.StartTimer();
                Quaternion landingRot = Quaternion.FromToRotation(Vector3.up,hit.normal);
                landingRot.y = player.transform.localRotation.y;
                player.playerRb.angularVelocity = Vector3.zero;
                player.playerRb.DORotate(landingRot.eulerAngles,perfectLanding.elapsedPercent);
                
                Debug.Log("Perfect landing");
                var velocityDir = player.playerRb.velocity.normalized;
                PerfectLandingBoost(velocityDir, 800);
                //play effect
                GameManager.instance.effectsManager.PlayParty(player.transform.position, Quaternion.FromToRotation(Vector3.up,hit.normal), 1,player.transform);
                //Update gained score!
                ScoringManager.OnGainingScore(currentScore);
                //reset current score to zer0
                ResetCurrentScore();
                succesfulLanding = true;
                player.animator.SetTrigger(AnimID.landing);
            }else
            {
                ScoringManager.OnHideScore();
                var ball = player.ball.material;
                GameManager.instance.effectsManager.FlashMaterial(ball,Color.black);
                Debug.Log("Sloppy landing");
            }   
        } 
        
        
        if(!succesfulLanding) 
        {
            ScoringManager.OnHideScore();
            var ball = player.ball.material;
            GameManager.instance.effectsManager.FlashMaterial(ball,Color.black);
            Debug.Log("Sloppy landing");
        }
        player.previousState.OnEnterState();
        
   }

   void PerfectLandingBoost(Vector3 playerVelocityDir, float boostForce)
   {
        var boostDir = new Vector3(playerVelocityDir.x, 0, playerVelocityDir.z);
        boostDir = new Vector3(Mathf.Round(boostDir.x),Mathf.Round(boostDir.y), Mathf.Round(boostDir.z));
       Debug.Log("boost dir" + boostDir);
       player.playerRb.AddForce(boostDir * boostForce,ForceMode.Impulse);
       //player.playerRb.velocity = playerVelocityDir * 100;
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
