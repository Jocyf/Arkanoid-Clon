using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public enum MainMenuType {  NEWGAME = 0, CONTINUE = 1, SCORE = 2 }
    public MainMenuType mainMenuTypeButton = MainMenuType.NEWGAME;

    public void ButtonPressed ()
    {
		switch(mainMenuTypeButton)
        {
            case MainMenuType.NEWGAME:
                ArkanoidManager.Instance.gameContainer.SetActive(true);
                ArkanoidManager.Instance.StartNewGame();
                ArkanoidManager.Instance.inGameUI.SetActive(true);
                ArkanoidManager.Instance.mainMenu.SetActive(false);
                ArkanoidManager.Instance.scoreMenu.SetActive(false);
                ArkanoidManager.Instance.pauseMenu.SetActive(false);
                break;
            case MainMenuType.CONTINUE:
                ArkanoidManager.Instance.gameContainer.SetActive(true);
                ArkanoidManager.Instance.ContinueGame();
                ArkanoidManager.Instance.inGameUI.SetActive(true);
                ArkanoidManager.Instance.mainMenu.SetActive(false);
                ArkanoidManager.Instance.scoreMenu.SetActive(false);
                ArkanoidManager.Instance.pauseMenu.SetActive(false);
                break;
            case MainMenuType.SCORE:
                ArkanoidManager.Instance.gameContainer.SetActive(false);
                ArkanoidManager.Instance.inGameUI.SetActive(false);
                ArkanoidManager.Instance.mainMenu.SetActive(false);
                ArkanoidManager.Instance.scoreMenu.SetActive(true);
                ArkanoidManager.Instance.pauseMenu.SetActive(false);
                break;
        }
	}
	
}
