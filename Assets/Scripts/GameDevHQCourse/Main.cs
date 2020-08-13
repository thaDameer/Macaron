using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public delegate void Teleport(Vector3 position);
    public static event Teleport onTeleport;
    
    public Transform obj;
    public Timer timer = new Timer(1);
    Func<string,string,int> CharacterLenght;
    Func<string,string,string> NameAndLastname;
    public string name;
    public string lastName;
    float timeDebug = 0;
    void Start()
    {
        NameAndLastname = (a,b) =>
        {
            return a +" "+ b;
        };
        CharacterLenght = (x,y) =>  x.Length + y.Length;
        int count = CharacterLenght(name,lastName);
        Debug.Log(NameAndLastname(name,lastName));  
        
        //Add a lambda expressions as an action
        StartCoroutine(MyRoutine(()=>
        {
            Debug.Log("the coroutine is finished using lambda expression");
        }));
        //Add a existing void function as an action
        StartCoroutine(MyRoutine(DisplayText));
    }

    // Update is called once per frame
    Tweener tweener;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var randomPos = new Vector3(Random.Range(-5,5),Random.Range(-5,5), Random.Range(-5,5));
            timer.StartTimer();
            Debug.Log(timer.elapsedTime);

            tweener = obj.DOMove(randomPos,1f);
            if(onTeleport != null)
            {
                // if(!timer.isTimerElapsed)
                // {
                //     var pos = new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-2,2));
                //     onTeleport(pos);
                //     tweener.Kill();
                // }

            }
        }
        if(!timer.isTimerElapsed)
        {
            Debug.Log(timer.elapsedPercent);
        }else
        {
            tweener.Kill();

        }
        // if(timer.isTimerElapsed)
        // {
        //     Debug.Log("TIME STOP");
        // }else
        // {
        // timeDebug += Time.deltaTime;
        // Debug.Log(timer.elapsedTime);
        // Debug.Log(timer.elapsedPercent);
        

        // }
    }
        IEnumerator MyRoutine(Action onComplete)
        {
            yield return new WaitForSeconds(5);
            if(onComplete!=null)
            {
                onComplete();
            }
        } 
        void DisplayText()
        {
            Debug.Log("Display the complete text");
        }
}  
