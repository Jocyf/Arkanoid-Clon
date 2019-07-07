using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

    public void ButtonPressed()
    {
        //ArkanoidManager.Instance.gameContainer.SetActive(true);
        //ArkanoidManager.Instance.inGameUI.SetActive(true);
        ArkanoidManager.Instance.Pause();
        ArkanoidManager.Instance.mainMenu.SetActive(false);
        ArkanoidManager.Instance.scoreMenu.SetActive(false);
        ArkanoidManager.Instance.pauseMenu.SetActive(true);
    }

}
