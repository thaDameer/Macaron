using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Main : MonoBehaviour
{
    public delegate void Teleport(Vector3 position);
    public static event Teleport onTeleport;
    
    public Transform obj;
    public Timer timer = new Timer(1);
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            timer.StartTimer();
            var randomPos = new Vector3(Random.Range(-5,5),Random.Range(-5,5), Random.Range(-5,5));

            obj.DOMove(randomPos,timer.elapsedPercent);
            if(onTeleport != null)
            {
                if(!timer.isTimerElapsed)
                {
                    var pos = new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-2,2));
                    onTeleport(pos);
                    
                }

            }
        }


        if(!timer.isTimerElapsed)
        {
            Debug.Log(timer.elapsedTime);
        }
    }
}
