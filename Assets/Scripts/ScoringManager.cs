using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoringManager : MonoBehaviour
{
    public static Action<float> OnScoring; 
    public static Action<float> OnGainingScore;
    public float gainedScore;
    private void OnEnable() 
    {
        OnScoring += CurrentScore;    
    }
    private void OnDisable() {
        OnScoring-=CurrentScore;
    }
    public void CurrentScore(float current)
    {
        gainedScore = current;
    }
}
