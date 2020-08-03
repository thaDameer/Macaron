using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Actor
{
    public static GameManager instance;
#region Managers and Handlers
    //controls the main camera and what camera mode it should be in
    public CameraManager cameraManager;
    //keeps track of levelID and how many tries each level has
    public StageHandler levelHandler;
    //controls everything UI
    public UIManager uiManager;
    //used to switch scenes
    public SceneHandler sceneHandler;
#endregion
    public Player player;

#region  Game Manager States
    public GameWon sGameWon{get; protected set;}
    public GameLost sGameLost{get; protected set;}
    public GamePaused sGamePaused{get; protected set;}
    public GamePlaying sGamePlaying{get; protected set;}
#endregion
    private int currentLevel
    {
        get {return levelHandler.levelId;}
        set{currentLevel = value;}
    }
    private int currentLives
    {
        get {return levelHandler.amountOfBalls;}
        set{currentLives = value;}
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        levelHandler = GameObject.Find("LevelHandler").GetComponent<StageHandler>(); 
        if(!levelHandler)
        {
            //If level handler doesn`t exist in scene, stop the game!
            Debug.Assert(levelHandler, "Level handler don`t exist in scene!");
        }
        SetupGame();    
    } 
    void SetupGame()
    {
        sGameLost = new GameLost(this);
        sGamePaused = new GamePaused(this);
        sGamePlaying = new GamePlaying(this);
        sGameWon = new GameWon(this);
        if(levelHandler)
        {
            levelHandler.Init();
            Debug.Log(currentLevel);
            sGamePlaying.OnEnterState();
        }
    }
    ///<summary>Loads the next scene in the build Index</summary>
    public void LoadNextLevel()
    {
        int nextLevel = currentLevel;
        nextLevel++;
        string levelName = "level "+nextLevel;
        sceneHandler.WhatSceneToLoad(levelName);
    }
}
