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
    }
    public override void OnExitState()
    {
        base.OnExitState();
    }
    public override void Update()
    {
        base.Update();
    }
}
