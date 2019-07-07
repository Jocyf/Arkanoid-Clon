using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketAutoIlum : MonoBehaviour {

    public float ilumTime = 0.15f;

    private Sprite normalSprite;
    public Sprite ilumSprite;

    private SpriteRenderer mySpriteRenderer;


    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = mySpriteRenderer.sprite;
    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.tag.Contains("Ball")) { return; }

        IlumRacket();
    }*/

        public void IlumRacket()
    {
        mySpriteRenderer.sprite = ilumSprite;
        StartCoroutine("_StopIlumRacket");
    }

    IEnumerator _StopIlumRacket()
    {
        yield return new WaitForSeconds(ilumTime);
        mySpriteRenderer.sprite = normalSprite;
    }
}
