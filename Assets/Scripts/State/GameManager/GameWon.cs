using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;
public class GameWon : State
{
    
    GameManager gm;
    public GameWon(GameManager actor) : base(actor)
    {
        gm = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        gm.uiManager.winScreen.Show();
    }
    public override void OnExitState()
    {
        base.OnExitState();
        gm.uiManager.winScreen.Hide();
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gm.LoadNextLevel();
        }
    }
}
