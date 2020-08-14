using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneLoading;

public class GameManager : Actor
{
    public static GameManager instance;
#region Managers and Handlers
    //controls the main camera and what camera mode it should be in
    public CameraManager cameraManager;
    public EffectsManager effectsManager;
    //keeps track of levelID and how many tries each level has
    public SceneHandler sceneHandler;
    //controls everything UI
    public UIManager uiManager;
    //used to switch scenes
    public LoadManager loadManager;
#endregion
    public Player player;

#region  Game Manager States
    public GameWon sGameWon{get; protected set;}
    public GameLost sGameLost{get; protected set;}
    public GameMainMenu sGamePaused{get; protected set;}
    public GamePlaying sGamePlaying{get; protected set;}
    public GameMissedShot sGameMissedShot{get; protected set;}
    public GameMainMenu sGameMenu {get; protected set;}
#endregion
    private int currentLevel
    {
        get {return sceneHandler.levelId;}
        set{currentLevel = value;}
    }
    private int lifeCount;
    public bool isMainMenu {get{return sceneHandler.isMenuScene;}}
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        //Set game manager states
        sGameLost = new GameLost(this);
        sGameMenu = new GameMainMenu(this);
        sGamePlaying = new GamePlaying(this);
        sGameWon = new GameWon(this);
        sGameMissedShot = new GameMissedShot(this);

        //check and see if its main menu
       
        sceneHandler = GameObject.Find("LevelHandler").GetComponent<SceneHandler>(); 
        
        if(sceneHandler && !sceneHandler.isMenuScene)
        {
            //else look for the level handler for the gameplay stages
            if(sceneHandler) {SetupGame();} 
        } 
        else
        {
            //do something
            sGameMenu.OnEnterState();
        
        }
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
        
        if(sceneHandler)
        {
            sceneHandler.Init();           
            sGamePlaying.OnEnterState();
            lifeCount = sceneHandler.amountOfBalls;
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
        LoadManager.OnLoadNewScene(nextLevel.ToString(),LoadMode.Delay);
    }
}
