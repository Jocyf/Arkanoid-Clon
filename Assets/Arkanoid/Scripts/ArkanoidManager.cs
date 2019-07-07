using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArkanoidManager : MonoBehaviour
{
    public bool reset = false;

    [Space(5)]
    public GameObject gameContainer;
    public GameObject blockContainer;

    public int currentLevel = 1;
    public int numLevels = 100;

    [Header("Rackets")]
    public GameObject racket;
    public GameObject ball;

    [Header("Score")]
    public int lives = 3;
    public int score = 0;
    private int inGameScore = 0;

    [Header("UI Configuration")]
    public Text scoreText;
    public Text liveText;
    public Text levelText;

    [Space(5)]
    public GameObject inGameUI;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject scoreMenu;


    [Header("Runtime Watchers")]
    public bool isPlaying = false;
    public bool isPause = false;

    private List<Block> blockList = new List<Block>();
    private int numBlocks = 0;

    private Vector3 racketSclOrig;


    #region Singleton
    public static ArkanoidManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion 


    public void ReStartGame()
    {
        ResetRacket();
        ResetBall();
        PickupMngr.Instance.DestroyAllPickups();

        isPlaying = true;
    }


    public void StartNewGame()
    {
        ResetData();
        ResetScore();
        ResetRacket();
        ResetBall();
        SkillMngr.Instance.ResetSkill();
        LoadLevel();

        isPlaying = true;

        UpdateLevelUIText();
        UpdateLiveUIText();
        UpdateScoreUIText();
    }

    public void ContinueGame()
    {
        inGameScore = score;
        ResetRacket();
        ResetBall();
        SkillMngr.Instance.UpdateSkill();
        LoadLevel();

        isPlaying = true;

        UpdateLevelUIText();
        UpdateLiveUIText();
        UpdateScoreUIText();
    }


    public void ExitGame()
    {
        ResetRacket();
        ResetBall();
        PickupMngr.Instance.DestroyAllPickups();

        isPlaying = false;
        DestroyAllBlocks();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        isPause = false; ;
    }

    public void Die()
    {
        lives--;    // lives = lives - 1;    lives -= 1;
        if (lives <= 0)
        {
            Debug.Log("Die. Lives is zero. Is Game Over.");
            ExitGame();
            LoadMainmenu();
        }
        else
        {
            Debug.Log("Die. You still have lives. Restart the game.");
            ReStartGame();
        }

        UpdateLiveUIText();
    }


    public void LoadMainmenu()
    {
        gameContainer.SetActive(false);
        inGameUI.SetActive(false);
        mainMenu.SetActive(true);
        scoreMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }


    public void DestroyBlock()
    {
        numBlocks--; // numBlocks = numBlocks -1;   numBlocks -= 1;
        if (IsLevelFinished())
        {
            Debug.Log("Level Finished. Load next level.");
            currentLevel++;
            score = inGameScore;
            SaveData();

            UpdateLevelUIText();
            PickupMngr.Instance.DestroyAllPickups();
            ContinueGame();
            //LoadNextLevel();
        }
    }


    public void DestroyAllBlocks()
    {
        numBlocks = 0;
        blockList.Clear();

        Block[] blocks = blockContainer.GetComponentsInChildren<Block>();
        for(int i = 0; i < blocks.Length; i++)
        {
            Destroy(blocks[i].gameObject);
        }

        
    }

    public bool IsLevelFinished()
    {
        return numBlocks == 0;

        /*if (numBlocks == 0)
            return true;
        else if (numBlocks != 0)
            return false;*/
    }


    void Start()
    {
        isPlaying = false;
        isPause = false;
        gameContainer.SetActive(false);
        racketSclOrig = racket.transform.localScale;
        //LoadData();

        DestroyAllBlocks();
        //LoadData();
    }


    public void LoadBlocks()
    {
        Block[] blocks = blockContainer.GetComponentsInChildren<Block>();
        for (int i = 0; i < blocks.Length; i++)
        {
            blockList.Add(blocks[i]);
            if (blocks[i].numCols != 0) // Bloque destruible
            {
                numBlocks++;
            }
        }
    }

    public void LoadLevel()
    {
        gameContainer.BroadcastMessage("LoadMap", currentLevel);
        LoadBlocks();
    }


    public void ResetBall()
    {
        ball.transform.parent = racket.transform;
        ball.SendMessage("ResetBall");
    }

    public void ResetRacket()
    {
        racket.GetComponent<RacketMovement>().enabled = true;
        racket.GetComponent<RacketIA>().enabled = false;
        racket.transform.position = new Vector2(0f, -4.39f);
        racket.transform.localScale = racketSclOrig;
    }

    private void ResetScore()
    {
        score = 0;
        inGameScore = 0;
        UpdateScoreUIText();
    }

    public void AddScore(int points)
    {
        inGameScore += points;
        UpdateScoreUIText();
    }



    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
        LoadData();
    }

    private void LoadData()
    {
        score = PlayerPrefs.GetInt("score", 0);
        lives = PlayerPrefs.GetInt("lives", 3);
        currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("lives", lives);
        PlayerPrefs.SetInt("currentLevel", currentLevel);
    }



    private void UpdateScoreUIText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + inGameScore.ToString();
        }
    }

    private void UpdateLiveUIText()
    {
        if (liveText != null)
        {
            liveText.text = "Lives: " + lives.ToString();
        }
    }

    private void UpdateLevelUIText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + currentLevel.ToString();
        }
    }

}
