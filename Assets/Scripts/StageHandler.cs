using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHandler : MonoBehaviour
{
    public CameraMode CAMERA_MODE;
  
    public Transform startPosition;
    public GoalScript goalPosition;
    private Player ballPrefab;
    private List<Player> listOfBalls = new List<Player>();
    public int amountOfBalls = 3;
    int currentBall = 0;
    public int levelId;
    public bool isLevelFinished = false;
 
    public void Init()
    {
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
        listOfBalls[currentBall].gameObject.SetActive(true);
        GameManager.instance.cameraManager.UpdateObjectToFollowAndMode(listOfBalls[currentBall].transform, CAMERA_MODE);
        //set active ball
        UpdateActiveBall();
        
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            listOfBalls[currentBall].sPlayerDead.OnEnterState();
            //SpawnANewBall();
        }     
    }

    public void UpdateActiveBall()
    {
        //set the ball position to the start position
        listOfBalls[currentBall].transform.position = startPosition.position;
        GameManager.instance.player = listOfBalls[currentBall];
    }

    public void SpawnANewBall()
    {
        currentBall++;
        if(currentBall >= listOfBalls.Count)return;
        listOfBalls[currentBall].gameObject.SetActive(true);
        GameManager.instance.cameraManager.UpdateObjectToFollowAndMode(listOfBalls[currentBall].transform, CAMERA_MODE);
        UpdateActiveBall();
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
    
    #endregion
}


