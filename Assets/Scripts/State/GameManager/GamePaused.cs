using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class GamePaused : State
{
   
    GameManager gm;
    public GamePaused(GameManager actor) : base(actor)
    {
        gm = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        gm.uiManager.pauseScreen.Show();
    }
    public override void OnExitState()
    {
        gm.gamePaused = false;
        gm.uiManager.pauseScreen.Hide();
        base.OnExitState();
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gm.previousState.OnEnterState();
        }
    }
}
