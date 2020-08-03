using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class GameMissedShot : State
{
    GameManager gm;
    Coroutine cameraCR;
    public GameMissedShot(GameManager actor) : base(actor)
    {
        gm=actor;
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        StartStateCoroutine(gm.cameraManager.ReturnToStartPosition());
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
