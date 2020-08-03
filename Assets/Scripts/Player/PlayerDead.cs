using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;
public class PlayerDead : State
{
    Player player;
    float counter;
    float delayTime = 1.5f;
    public PlayerDead(Player actor) : base(actor)
    {
        player = actor;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        player.ballObject.layer = 0;
        counter = delayTime;
        StartStateCoroutine(DelayDeath());
    }
    IEnumerator DelayDeath()
    {
        while(counter>0)
        {
            counter -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GameManager.instance.levelHandler.SpawnANewBall();

    }
}
