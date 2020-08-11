using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScoringScreen : UIBase
{
   public Text gainedScore, scoringText;
   bool scoreIsActive;
   public override void Awake()
   {
       base.Awake();
       Show();
       Color startCol = new Color(255,255,255,0);
       gainedScore.color = startCol;
       scoringText.color = startCol;
   }
   private void Start() {
       if(gainedScore)
       {
        ScoringManager.OnScoring += UpdateRealtimeScore; 
        ScoringManager.OnGainingScore += VisualizeGainedScore;
        ScoringManager.OnHideScore += HideAndDecrease;
       }
       
   }

    public Timer routineTimer = new Timer(0.2f);
    public void VisualizeGainedScore(float score)
    {
        routineTimer.StartTimer();
        Vector3 upscaleVector = new Vector3(2f,2f,2f);
        var rectTransform = gainedScore.GetComponent<RectTransform>();
        // DOTween.To(()=> tweenedValue, x=> tweenedValue = x, score, routineTimer.elapsedPercent).onComplete();
        if(rectTransform)
        {
            rectTransform.DOScale(upscaleVector ,routineTimer.elapsedPercent).OnComplete(HideAndDecrease);   
            rectTransform.DOShakeAnchorPos(routineTimer.elapsedPercent,10,3,10,false,true); 
        }
        gainedScore.text = Mathf.RoundToInt(score).ToString();
        
    }
    public void HideAndDecrease()
    {
        scoreIsActive = false;
        routineTimer = new Timer(8);
        routineTimer.StartTimer();
        var rectTransform = gainedScore.GetComponent<RectTransform>();
        if(rectTransform)
        {
            rectTransform.DOScale(Vector3.one ,routineTimer.elapsedPercent);
        }
        gainedScore.DOColor(new Color(255,255,255,0),routineTimer.elapsedPercent);//.OnComplete(Hide);
        scoringText.DOColor(new Color(255,255,255,0),routineTimer.elapsedPercent);
    }
    
    public void UpdateRealtimeScore(float currentScore)
    {  
        if(!scoreIsActive)
        {
            //Show();
            scoreIsActive = true;
        }
        routineTimer = new Timer(.4f);
        routineTimer.StartTimer();
        gainedScore.DOColor(new Color(255,255,255,1),routineTimer.elapsedPercent);
        scoringText.DOColor(new Color(255,255,255,1),routineTimer.elapsedPercent);
        gainedScore.text = Mathf.RoundToInt(currentScore).ToString();
    }
    private void OnDisable() 
    {
        ScoringManager.OnScoring-=UpdateRealtimeScore;   
        ScoringManager.OnGainingScore -= VisualizeGainedScore; 
        ScoringManager.OnHideScore -= HideAndDecrease;
        scoreIsActive = false;
    }
}
