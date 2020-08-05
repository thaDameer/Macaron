using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuby : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    bool isSmashed = false;
    public float explosionPower = 50f;

    private void OnTriggerEnter(Collider other) 
    {
        var player = other.GetComponentInParent<Player>();
        if(player && !isSmashed)
        {

            Debug.Log(player.playerRb.velocity.magnitude);
            player.playerRb.AddForce(Vector3.forward * 200,ForceMode.Impulse);
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = false;
                
                rigidbodies[i].AddExplosionForce(player.playerRb.velocity.magnitude *2, player.transform.position,20);
                Destroy(rigidbodies[i].gameObject,Random.Range(2,5));
            }
            isSmashed = true;
        }    
    }
}
