using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenuLabel : MonoBehaviour {

    public enum ScoreMenuType { LEVEL = 0, LIVES = 1, SCORE = 2 }
    public ScoreMenuType scoreMenuTypeLabel = ScoreMenuType.LEVEL;

    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
        WriteData();
    }

    private void WriteData()
    {
        switch (scoreMenuTypeLabel)
        {
            case ScoreMenuType.LEVEL:
                //myText.text = "Level: " + ArkanoidManager.Instance.currentLevel;
                break;
            case ScoreMenuType.LIVES:
                myText.text = "Lives: " + ArkanoidManager.Instance.lives.ToString();
                break;
            case ScoreMenuType.SCORE:
                myText.text = "Score: " + ArkanoidManager.Instance.score.ToString();
                break;
        }
    }

}
