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
        get 
        {
            Update();    
            return timer;
        } 
    }
   public float remainingTime => duration - elapsedTime; 

   
    public bool isTimerElapsed => elapsedTime >= duration;
    public float elapsedPercent
    {
        get
        {
            return Mathf.Clamp01(elapsedTime/elapsedTime);
        }
    }

    bool isRunning;
    public void StartTimer()
    {
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
