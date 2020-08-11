using UnityEngine;


public struct Timer 
{
    public Timer(float lifespan, bool loop = false)
    {
        duration = lifespan;
        isRunning = false;
        timer = 0;
    }
    private float duration;
    float timer;
    public float elapsedTime
    {
        get{return timer;}
    }
    float ElapsedTime 
    {
        get 
        {
            Update();   
            return timer;
        } 
    }
   public float remainingTime => duration - ElapsedTime; 

   
    public bool isTimerElapsed => ElapsedTime >= duration;
    public float elapsedPercent
    {
        get
        {
            return Mathf.Clamp01(elapsedTime/duration);
        }
    }

    bool isRunning;
    public void StartTimer(bool reset = true)
    {
        if(reset)
        {
            isRunning = false;
            timer = 0;
        }
        isRunning = true;
        Update();
    }
    
    private void Update() 
    {
        if(isRunning)
        {
            timer+=Time.deltaTime;
        }
    }
}
