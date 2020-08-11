using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void WhatSceneToLoad(string nameToScene)
    {
        //Maybe do this in a coroutine and add a faded vignette or something?
        var sceneName =SceneManager.GetSceneByName(nameToScene);
        //if(!sceneName.IsValid()) return;
        SceneManager.LoadScene(nameToScene);
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
