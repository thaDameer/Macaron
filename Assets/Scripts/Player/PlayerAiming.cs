using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class PlayerAiming : State
{
    Player player;

    public PlayerAiming(Player actor) : base(actor)
    {
        player = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        player.AimingTools(true);
        player.mouseDownSprite.transform.localPosition = Vector3.zero;
    }

    public override void OnExitState()
    {
        base.OnExitState();
        player.AimingTools(false);
        player.animator.SetFloat(AnimID.stretch, 0);
        //player.ActivateAimingArrow(false);
        player.mouseDownSprite.transform.localPosition = Vector3.zero;
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetMouseButton(0))
        {
            Aiming();
        } 
        else if(Input.GetMouseButtonUp(0) && !canShoot)
        {
            //Player returns to idle state
            player.sPlayerIdle.OnEnterState();

        } else if(Input.GetMouseButtonUp(0) && canShoot)
        {
            //Player shoots
            
            GameManager.instance.cameraManager.CameraShake();
            player.animator.SetTrigger(AnimID.scaleUp);
            player.sPlayerShoots.OnEnterState();
        }
    }
    Vector3 newLocation;
    bool canShoot = false;
    void Aiming()
    {
        var center = player.transform.position;
        Ray mousePos =  GameManager.instance.cameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(mousePos,out hit,Mathf.Infinity))
        {
            float distance = Vector3.Distance(center, hit.point);
            player.animator.SetFloat(AnimID.stretch,distance);
            if(distance > 1)
            {
                canShoot = true;
            } else
            {
                canShoot = false;
            }            
            if(distance < player.aimRadius)
            {
                newLocation = new Vector3(hit.point.x, 0.5f, hit.point.z);
                player.mouseDownSprite.transform.position = newLocation;
                AimingArrow(distance,center, hit.point);
            }
            else
            {
                newLocation = new Vector3(hit.point.x, 0.5f, hit.point.z);
                Vector3 fromOriginToObject = newLocation - center;
                fromOriginToObject *= player.aimRadius / distance;
                newLocation = center + fromOriginToObject;
                player.mouseDownSprite.transform.position = newLocation; 
                //Aim radius = max multiplier speed
                AimingArrow(player.aimRadius,center,newLocation);
            }

        }
    }
    void AimingArrow(float chargeAmount,Vector3 centerPos , Vector3 mousePos)
    {
        mousePos.y = player.transform.position.y;
        var shootDirection = (centerPos-mousePos).normalized;

        var rotation = Quaternion.LookRotation(shootDirection,player.aimingObject.transform.up);
        player.aimingObject.transform.localScale = new Vector3(player.aimingObject.transform.localScale.x,player.aimingObject.transform.localScale.y, chargeAmount);

        player.aimingObject.transform.rotation = rotation;
        
        player.movingDirection = shootDirection;
        player.multiplierSpeed = chargeAmount;
        //look at the same direction as the aiming arrow
        PlayerLookAt(mousePos);
    }

    void PlayerLookAt(Vector3 target)
    {
        Vector3 dir =  player.transform.position -target;
        Quaternion rotation = Quaternion.LookRotation(dir);
        rotation.x = 0;
        rotation.z = 0;
        
        player.transform.rotation = rotation;
    }
}
