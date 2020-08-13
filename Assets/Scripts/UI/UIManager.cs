using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region UI Screens
    public WinScreen winScreen;
    public LifeCounterScreen lifeCounter;
    public LostScreen lostScreen;
    public PauseScreen pauseScreen;
    #endregion

    public GameObject mainMenu;

    public void ShowMainMenu(bool isShown)
    {
        if(isShown)
        {
            mainMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }
}
