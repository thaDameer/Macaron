using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoringScreen : UIBase
{
   public Text scoreText;
   public override void Awake()
   {
       base.Awake();
   }
   private void Start() {
       if(scoreText)
       {
        ScoringManager.OnScoring += UpdateScore; 
       }
       
   }

 
    bool textIsActive = false;
    public void UpdateScore(float currentScore)
    {
        if(!textIsActive)
        {
            Show();
            textIsActive = true;
        }
        scoreText.text = Mathf.RoundToInt(currentScore).ToString();
    }
    private void OnDisable() 
    {
        ScoringManager.OnScoring-=UpdateScore;    
    }
}
