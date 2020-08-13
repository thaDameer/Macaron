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
    public void WhatSceneToLoad(string levelIDstring, LoadMode mode)
    {
        string sceneToLoad = "level "+levelIDstring;
         var sceneName =SceneManager.GetSceneByName(levelIDstring);
         Debug.Log(sceneName);
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
    IEnumerator LoadSceneDelayed(string scene)
    {
        //Maybe do this in a coroutine and add a faded vignette or something?
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }
    private void OnEnable() {
        OnLoadNewScene += WhatSceneToLoad;
    }
    private void OnDisable() {

        OnLoadNewScene -= WhatSceneToLoad;    
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