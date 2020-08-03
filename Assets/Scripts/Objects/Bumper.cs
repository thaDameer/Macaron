using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public Animator animator;
    public float bumpForce = 200f;
    public float maxForce = 1000;
    bool canBump = true;
    public ParticleSystem party;
    private void OnTriggerEnter(Collider other) 
    {
        // Debug.Log(other.gameObject.name);
        // Player player = other.GetComponentInParent<Player>();
        // if(player && canBump)
        // {
        //     animator.SetTrigger("Bump");
        //     var dir = (transform.position - player.transform.position).normalized;
        //     var reflectDir = Vector3.Reflect(dir, Vector3.up);
        //     //player.playerRb.AddForce(-dir * bumpForce,ForceMode.Impulse);
        //     player.playerRb.velocity = reflectDir * bumpForce;
        //     canBump = false;
        // }    
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        var player = other.gameObject.GetComponentInParent<Player>();
        if(player && canBump)
        {
            Bounce(other.contacts[0].normal,player);
            canBump = false;
        }   
    }

    private void Bounce (Vector3 collisionNormal, Player p)
    {
        animator.SetTrigger("Bump");
        party.Emit(20);
        var dir = Vector3.Reflect(p.playerRb.velocity.normalized,collisionNormal);
        Debug.Log("out direction "+ dir);
        dir.y = collisionNormal.y;
     
        float maxForce = Mathf.Max(p.playerRb.velocity.magnitude * 2, bumpForce);
        maxForce = Mathf.Clamp(maxForce,bumpForce,maxForce);
        //reset player rb
        p.playerRb.velocity = Vector3.zero;
        p.playerRb.angularVelocity =Vector3.zero;
        //Debug.Break();
        //p.playerRb.AddForce(dir * maxForce,ForceMode.Impulse);
        p.playerRb.velocity = dir * maxForce;

        Debug.Log("player velocity "+ p.playerRb.velocity.magnitude);
        Debug.Log("player  angVelocity"+ p.playerRb.angularVelocity);
    }
    public void ResetBump_AE()
    {
        canBump = true;
    }
}
