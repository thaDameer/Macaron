using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public CameraMode CAMERA_MODE;
    public bool isMenuScene;
    public Transform deadzoneParent;
    public Transform startPosition;
    public GoalScript goalPosition;
    [HideInInspector]
    public DeadZone upDeadzone,leftDeadzone,rightDeadzone;
    private Player ballPrefab;
    private List<Player> listOfBalls = new List<Player>();
    private Player activeBall;
    public int amountOfBalls = 3;
    int currentBall = 0;
    public int levelId;
    public bool isLevelFinished = false;
 
    public void Init()
    {
        if(isMenuScene) return;
        if(startPosition == null)
        {
            Debug.Assert(startPosition, "Start position hasn`t been set in LevelHandler");

        }
        //get ball prefab from resources folder and create a list of balls.
        ballPrefab = Resources.Load<Player>("Ball");
        for (int i = 0; i < amountOfBalls; i++)
        {
            var ballClone = Instantiate(ballPrefab, transform);
            listOfBalls.Add(ballClone);
            ballClone.gameObject.SetActive(false);
        }
        HideDeadZones();
    }

  
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.R) && activeBall && currentBall < listOfBalls.Count)
        {
            activeBall.sPlayerDead.OnEnterState();
            //SpawnANewBall();
        }     
    }

    public void SetActiveBallPos()
    {
        //set the ball position to the start position
        listOfBalls[currentBall].transform.position = startPosition.position;
        GameManager.instance.player = listOfBalls[currentBall];
    }

    public void SpawnANewBall()
    {
        activeBall = listOfBalls[currentBall];
        activeBall.gameObject.SetActive(true);
        activeBall.isAlive = true;        
        GameManager.instance.cameraManager.UpdateObjectToFollowAndMode(activeBall.transform, CAMERA_MODE);
        SetActiveBallPos();
        currentBall++;
    }
    public void LevelIsFinished()
    {
        Debug.Log("finished");
        isLevelFinished = true;
        GameManager.instance.sGameWon.OnEnterState();
    }

    #region  Editor
    public void CreatePositionObjects(string whatObject)
    {
        
        switch(whatObject)
        {
            case "start":
                var instantiatedStart = Instantiate(Resources.Load<Transform>("StartPos"),transform);
                startPosition = instantiatedStart;
            break;
            case "goal":
                var instantiatedGoal = Instantiate(Resources.Load<GoalScript>("Goal"),transform);
                goalPosition = instantiatedGoal;
            break;
            case "checkpoint":
            //use to instantiate checkpoints
            break;
        }
    }
    public void CreateDeadZones(ObjectRotation rotation)
    {
        if(!deadzoneParent) {Debug.Log("Add deadzone parent to stage handler in inspector"); return;}
        
        var obj = Instantiate(Resources.Load<DeadZone>("DeadZone"),deadzoneParent);
        obj.SetRotation(rotation);
        switch(rotation)
        {
            case ObjectRotation.LEFT:
                leftDeadzone = obj;
                leftDeadzone.name = "left deadzone";
                break;
            case ObjectRotation.RIGHT:
                rightDeadzone = obj;
                rightDeadzone.name ="right deadzone";
                break;
            case ObjectRotation.UP:
                upDeadzone = obj;
                upDeadzone.name = "up deadzone";
                break;
        }
    }
    
    public void HideDeadZones()
    {
        leftDeadzone.HideMesh();
        rightDeadzone.HideMesh();
        upDeadzone.HideMesh();
    }

    #endregion
}


