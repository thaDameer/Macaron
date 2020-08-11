using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public Animator animator;
    public float bumpForce = 200f;
    public float maxForce = 1000;
    bool canBump = true;
    public GameObject party;
    public Timer recoveryTimer = new Timer(0.09f);
    
    private void OnTriggerEnter(Collider other) 
    {
        var player = other.gameObject.GetComponentInParent<Player>();
        if(player && canBump)
        {
            var bumpDir = (player.transform.position - transform.position).normalized;
            Bounce(bumpDir, player);
            canBump = false;
        }
    }
    private void Update() 
    {
        if(!canBump)
        {
           if(recoveryTimer.isTimerElapsed)
           {    
               
                canBump = true;
           }
        }    
    }
    private void Bounce (Vector3 collisionNormal, Player p)
    {
        //Start party effect
        party.SetActive(false);
        GameManager.instance.cameraManager.CameraShake(ShakeType.Shake);
        var dir = Vector3.Reflect(p.playerRb.velocity.normalized,collisionNormal);
        dir.y = 0;
        //dir.y = collisionNormal.y;
     
        var relativeForce = Mathf.Max(p.playerRb.velocity.magnitude * 2, bumpForce); 
        maxForce = Mathf.Clamp(maxForce,bumpForce,relativeForce);
        Debug.Log(maxForce);
        //reset player rb
        p.playerRb.velocity = Vector3.zero;
        p.playerRb.angularVelocity =Vector3.zero;
        Debug.Log(dir);

        p.playerRb.velocity = dir * maxForce;
        animator.SetTrigger("Bump");
        party.SetActive(true);
        //start the recovery timer
        recoveryTimer.StartTimer();
    }
}
