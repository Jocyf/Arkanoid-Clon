using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMngr : MonoBehaviour {

    public int skill = 1;
    public int topBallSpeed = 10;

    private Ball BallScr;

    private float ballSpeedOrig;


    #region Singleton
    public static SkillMngr Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public void ResetSkill()
    {
        skill = 1;
        SetSkill();
    }

    // Cambia la velocidad de la bola
    private void SetSkill()
    {
        if(BallScr.speed < topBallSpeed)
        {
            BallScr.speed = ballSpeedOrig + skill * 0.1f;
        }
    }

    public void UpdateSkill()
    {
        skill = ArkanoidManager.Instance.currentLevel;
        SetSkill();
    }

    void Start ()
    {
        BallScr = ArkanoidManager.Instance.ball.GetComponent<Ball>();
        ballSpeedOrig = BallScr.speed;

        skill = ArkanoidManager.Instance.currentLevel;
    }
}
