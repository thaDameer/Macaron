using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoringManager : MonoBehaviour
{
    public static Action<float> OnScoring; 
    public static Action<float> OnGainingScore;
    public static Action OnHideScore;
    public float gainedScore;
    private void OnEnable() 
    {
        OnScoring += CurrentScore;    
        OnGainingScore+=StoreGainedScore;
        
    }
    private void OnDisable() {
        OnScoring-=CurrentScore;
        OnGainingScore-=StoreGainedScore;
    }
    public void StoreGainedScore(float score)
    {
        gainedScore = score;
    }
    public void CurrentScore(float current)
    {
        gainedScore = current;
    }
}
