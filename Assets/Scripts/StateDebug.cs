using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateDebug : MonoBehaviour
{
    public Text currentState,previousState, levelStatus, gameManagerStatus, currentLevel;
    public GameObject container;
    string debugText;
    bool isActive = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isActive)
            {
                //container.SetActive(false);
                Time.timeScale = 0;
                GameManager.instance.sGamePaused.OnEnterState();
                isActive = false;
                return;
            }
            else
            {
                container.SetActive(true);
                Time.timeScale = 1;
                GameManager.instance.previousState.OnEnterState();
                isActive = true;
                return;
            }
        } 
        
        
        if(GameManager.instance.player == null) return;
        if(GameManager.instance.player.previousState == null || GameManager.instance.player.currentState == null) return;
        currentState.text = GameManager.instance.player.currentState.ToString();
        previousState.text = GameManager.instance.player.previousState.ToString();
        gameManagerStatus.text = GameManager.instance.currentState.ToString();
        currentLevel.text =  "Level: "+GameManager.instance.levelHandler.levelId.ToString();
        if(GameManager.instance.levelHandler.isLevelFinished)
        {
            levelStatus.text = "YOU SMASHED IT!!!";
        }
    }
}
