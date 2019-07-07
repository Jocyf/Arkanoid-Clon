using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButton : MonoBehaviour {

    public enum MainMenuType { CONTINUE = 0, MAINMENU = 1 }
    public MainMenuType mainMenuTypeButton = MainMenuType.CONTINUE;

    public void ButtonPressed()
    {
        switch (mainMenuTypeButton)
        {
            case MainMenuType.CONTINUE:
                ArkanoidManager.Instance.gameContainer.SetActive(true);
                ArkanoidManager.Instance.UnPause();
                ArkanoidManager.Instance.inGameUI.SetActive(true);
                ArkanoidManager.Instance.mainMenu.SetActive(false);
                ArkanoidManager.Instance.scoreMenu.SetActive(false);
                ArkanoidManager.Instance.pauseMenu.SetActive(false);
                break;
            case MainMenuType.MAINMENU:
                ArkanoidManager.Instance.gameContainer.SetActive(false);
                ArkanoidManager.Instance.inGameUI.SetActive(false);
                ArkanoidManager.Instance.mainMenu.SetActive(true);
                ArkanoidManager.Instance.scoreMenu.SetActive(false);
                ArkanoidManager.Instance.pauseMenu.SetActive(false);
                Time.timeScale = 1;
                ArkanoidManager.Instance.ExitGame();
                break;
        }
    }
}
