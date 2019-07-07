using UnityEngine;

public class ScoreMenuButton : MonoBehaviour {

    public void ButtonPressed()
    {
        ArkanoidManager.Instance.gameContainer.SetActive(false);
        ArkanoidManager.Instance.inGameUI.SetActive(false);
        ArkanoidManager.Instance.mainMenu.SetActive(true);
        ArkanoidManager.Instance.scoreMenu.SetActive(false);
        ArkanoidManager.Instance.pauseMenu.SetActive(false);
    }
}
