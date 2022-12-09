using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public enum LoadMode
    {
        Instant,
        Delay
    }

public class LoadManager : MonoBehaviour
{
    public static Action<string,LoadMode> OnLoadNewScene;
    //stores the loaded level
    public void StoreAndLoadLevel(string levelID, LoadMode mode)
    {
        string sceneToLoad = "level "+levelID;
        if(mode == LoadMode.Instant)
        {
            SceneManager.LoadScene(sceneToLoad);
        }   
        if(mode == LoadMode.Delay)
        {
            GameManager.instance.effectsManager.FadeTransition("fadeIn");
            
            StartCoroutine(LoadSceneDelayed(sceneToLoad));
        }
    }
    private void StoreCurrentLevel(int levelToBeLoaded)
    {

    }
    IEnumerator LoadSceneDelayed(string scene)
    {
        //Maybe do this in a coroutine and add a faded vignette or something?
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }
    private void OnEnable() {
        OnLoadNewScene += StoreAndLoadLevel;
    }
    private void OnDisable() {

        OnLoadNewScene -= StoreAndLoadLevel;    
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
}