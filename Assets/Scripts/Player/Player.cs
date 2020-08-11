using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Actor
{
   public bool isAlive = false;
   [SerializeField]
   public float pathPosition;
   //EFFECTS
   public MeshRenderer ball;
   public Rigidbody playerRb;
   public GameObject ballObject;
   public GameObject aimingObject;
   public GameObject aimingRadiusCircle;
   public GameObject mouseDownSprite;
   public Vector3 movingDirection;
   public float shootSpeed = 3f;
   [HideInInspector]
   public float multiplierSpeed;
   public float flipSpeed = 8;
   public LayerMask playerLayer;
   public LayerMask groundLayer;
   public float aimRadius = 10;
   public ParticleSystem ps;
   
   
   //States 
   public PlayerAiming sPlayerAiming{get; protected set;}
   public PlayerIdle sPlayerIdle {get; protected set;}
   public PlayerShoots sPlayerShoots{get; protected set;}
   public PlayerDead sPlayerDead{get; protected set;}
   public PlayerAirborne sPlayerAirborne {get; protected set;}
   public PlayerGrinding sPlayerGrinding {get; protected set;}
   
   private void Awake() 
   {
      sPlayerAiming = new PlayerAiming(this);
      sPlayerIdle = new PlayerIdle(this);
      sPlayerShoots = new PlayerShoots(this);
      sPlayerDead = new PlayerDead(this);
      sPlayerAirborne = new PlayerAirborne(this);
      sPlayerGrinding = new PlayerGrinding(this);
      sPlayerIdle.OnEnterState();
   }

   public Vector2 movementInput;
   public float movementSpeed = 100;

   public override void Update()
   {
      if(!isAlive) return;
      base.Update();
      movementInput = new Vector2(Input.GetAxisRaw("Horizontal"),0);
   }

 
   public bool IsPointingAtPlayer()
   {
      RaycastHit hit;
      Ray ray = GameManager.instance.cameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
      var rayTest = Physics.Raycast(ray, out hit, Mathf.Infinity,playerLayer);
      return rayTest;
   }

   public void AimingTools(bool isVisible)
   {
      aimingRadiusCircle.SetActive(isVisible);
      aimingObject.SetActive(isVisible);
      mouseDownSprite.SetActive(isVisible);
   }
   public float groundCheckDist = 3;
   Vector3 sphereCheckPos;
   private void OnDrawGizmos() 
   {
      Debug.DrawRay(sphereCheckPos, -transform.up,Color.cyan,groundCheckDist);  
      Gizmos.color = new Color(1,0,0,0.5f);
      
      Gizmos.DrawSphere(transform.position,2);

   }
   public bool HasContactWithGround()
   {
      sphereCheckPos = transform.position;
      sphereCheckPos.y = transform.position.y + 0.5f;
      Collider[] colliders = Physics.OverlapSphere(sphereCheckPos,3, groundLayer);
      if(colliders.Length > 0)
      {
         return true;
      }else
      {
         return false;
      }
   }
   public bool IsPlayerGrounded()
   {  
      if(Physics.Raycast(transform.position,-transform.up,groundCheckDist,groundLayer))
      {
         return true;
      }else
      {
         return false;
      }
   }
   
   private void OnTriggerEnter(Collider other) 
   {
      var deadzone = other.GetComponentInParent<DeadZone>();
      if(deadzone && isAlive)
      {
         sPlayerDead.OnEnterState();
      }   
   }
}
