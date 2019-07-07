using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketIA : MonoBehaviour {

    public bool isUpRacket = true;
    public float speed = 10;

    public float rightBound = 3.2f;
    public float leftBound = -3.2f;

    private Rigidbody2D myRigidbody2D;
    private Transform ball;


    void Start ()
    {
        ball = GameObject.Find("Ball").transform;

        if (ball != null)
        {
            myRigidbody2D = ball.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.Log("WARNING: NO ENCUENTRO LA BOLA!!!!!");
        }
    }
	

	void Update ()
    {

        if ((myRigidbody2D.velocity.y > 0 && isUpRacket) || (myRigidbody2D.velocity.y < 0 && !isUpRacket))
        {
            if (ball.position.x < this.transform.position.x - 0.5f)
            {
                this.transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else if (ball.position.x > this.transform.position.x + 0.5f)
            {
                this.transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }

        // Seguridad: Bordes.
        if(transform.position.x > rightBound)
        {
            transform.position = new Vector2(rightBound, transform.position.y);
        }
        else if (transform.position.x < leftBound)
        {
            transform.position = new Vector2(leftBound, transform.position.y);
        }
    }
}
