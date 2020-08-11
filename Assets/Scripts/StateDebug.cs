using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateDebug : MonoBehaviour
{
    public Text currentState,previousState, gameManagerStatus, currentLevel;
    public GameObject container;
    string debugText;
    bool isActive = true;
    private void Start() {
        #if !UNITY_EDITOR
        container.SetActive(false);
        #endif        
    }
#if UNITY_EDITOR
    private void Update()
    {
        bool ctrlDown = Input.GetKey(KeyCode.RightShift);
        if(ctrlDown && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ok");
            if(isActive)
            {
                container.SetActive(false);
                isActive = false;
                return;
            }
            else
            {
                container.SetActive(true);
                isActive = true;
                return;
            }
        } 
        
        
        if(GameManager.instance.player == null) return;
        if(GameManager.instance.player.previousState == null || GameManager.instance.player.currentState == null) return;
        gameManagerStatus.text = GameManager.instance.currentState.ToString();
        currentState.text = GameManager.instance.player.currentState.ToString();
        previousState.text = GameManager.instance.player.previousState.ToString();
        currentLevel.text =  "Level: "+GameManager.instance.levelHandler.levelId.ToString();
        
    }
#endif
}
