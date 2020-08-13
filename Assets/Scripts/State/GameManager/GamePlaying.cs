using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;
public class GamePlaying : State
{
    
    GameManager gm;
    public GamePlaying(GameManager actor) : base(actor)
    {
        gm = actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        gm.sceneHandler.SpawnANewBall();
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
