using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) 
   {
        var player = other.GetComponentInParent<Player>();
        Debug.Log(other.name);
        if(player)
        {
            GameManager.instance.levelHandler.LevelIsFinished();
        }    
   }
}
