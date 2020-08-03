using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    bool isActive = false;
    public float boostPower = 100;
    float multipliedPower;
    private void OnTriggerEnter(Collider other) 
    {   
        Player player = other.gameObject.GetComponentInParent<Player>();
        
        if(player)
        {
           
            multipliedPower = boostPower + player.playerRb.velocity.magnitude;
            //player.playerRb.AddForce(transform.forward * boostPower,ForceMode.Impulse);
        }
        isActive = true;
    }



    float onTriggerTimeCount = 0;
    float maxTriggerTime = 0.5f;
    private void OnTriggerStay(Collider other) 
    {   
        Player player = other.gameObject.GetComponentInParent<Player>();
        if(player && isActive && onTriggerTimeCount < maxTriggerTime)
        {
            onTriggerTimeCount+=Time.deltaTime;
            var dir = (player.transform.position- transform.forward).normalized;
            //player.playerRb.velocity = transform.forward * boostPower;
            player.playerRb.AddForce(transform.forward * multipliedPower);
        }  
    }
    private void OnTriggerExit(Collider other) 
    {
        Player player = other.GetComponentInParent<Player>();
        if(player)
        {
            onTriggerTimeCount = 0;
        }
    }
}
