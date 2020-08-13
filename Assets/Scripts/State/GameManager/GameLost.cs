using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class GameLost : State
{
    GameManager gm;
    public GameLost(GameManager actor) : base(actor)
    {
        gm = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        gm.uiManager.lostScreen.Show();
    }
    public override void OnExitState()
    {
        base.OnExitState();
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gm.loadManager.RestartScene();
        }
    }
}
