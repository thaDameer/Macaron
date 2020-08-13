using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class GameMainMenu : State
{
    GameManager gm;
    public GameMainMenu(GameManager actor) : base(actor)
    {
        gm = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        //show the main menu
        gm.uiManager.ShowMainMenu(true);
        //turn off game Camera
        gm.cameraManager.mainCamera.gameObject.SetActive(false);
       
    }
    public override void OnExitState()
    {
        base.OnExitState();
        //hide the main menu
        gm.uiManager.ShowMainMenu(false);
        gm.cameraManager.mainCamera.gameObject.SetActive(true);
    }
    public override void Update()
    {
        base.Update();
        
    }
}
