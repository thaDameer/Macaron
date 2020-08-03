using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Actor
{
   bool isDead = false;
   [SerializeField]
   public float pathPosition;

   public Rigidbody playerRb;
   public GameObject ballObject;
   public GameObject aimingObject;
   public GameObject aimingRadiusCircle;
   public GameObject mouseDownSprite;
   public Vector3 movingDirection;
   public float shootSpeed = 3f;
   [HideInInspector]
   public float multiplierSpeed;
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
      if(isDead) return;
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
   
   public bool IsPlayerGrounded()
   {
      Collider[] collider = Physics.OverlapSphere(transform.position,3, groundLayer);
      if(collider.Length > 0)
      {
         return true;
      }
      else
      {
         return false;
      } 
   }
}
