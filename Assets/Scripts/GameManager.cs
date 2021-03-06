﻿using System.Collections;
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
    public GameMissedShot sGameMissedShot{get; protected set;}
#endregion
    private int currentLevel
    {
        get {return levelHandler.levelId;}
        set{currentLevel = value;}
    }
    private int lifeCount;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        //Set game manager states
        sGameLost = new GameLost(this);
        sGamePaused = new GamePaused(this);
        sGamePlaying = new GamePlaying(this);
        sGameWon = new GameWon(this);
        sGameMissedShot = new GameMissedShot(this);

        levelHandler = GameObject.Find("LevelHandler").GetComponent<StageHandler>(); 
        if(levelHandler) {SetupGame();}
    } 
    public bool gamePaused = false;
   
    public override void Update() 
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        { 
            gamePaused = true;
            PauseGame(gamePaused);
            return;
        } else if(Input.GetKeyDown(KeyCode.Escape) && gamePaused)
        {
            gamePaused = false;
            PauseGame(gamePaused);
            return;;
        }
    }
     private void PauseGame(bool isPaused)
    {
        if(isPaused)
        {
            Time.timeScale = 0;
            uiManager.pauseScreen.Show();
            return;
        }else
        {
            Time.timeScale = 1;
            uiManager.pauseScreen.Hide();
            return;
        }
    }

    void SetupGame()
    {
        
        if(levelHandler)
        {
            levelHandler.Init();           
            sGamePlaying.OnEnterState();
            lifeCount = levelHandler.amountOfBalls;
            uiManager.lifeCounter.SetupHearts(lifeCount);
        }
    }

    public void CurrentBallDied()
    {
        lifeCount--;
        uiManager.lifeCounter.LifeLost();
        if(lifeCount <= 0)
        {
            sGameLost.OnEnterState();
            return;
        }
        sGameMissedShot.OnEnterState();
        
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
