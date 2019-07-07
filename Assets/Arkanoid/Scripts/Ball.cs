using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed = 30;

    [Header("Ball Ilumination")]
    public float ilumTime = 0.15f;  // Tiempo Iluminado cuando golpea con algo
    private Sprite normalSprite;    //  Sprite normal
    public Sprite ilumSprite;       // Sprite Iluminado

    [Header("Ball Sounds")]
    public AudioClip wallHitFX;
    public AudioClip blockHitFX;
    public AudioClip racketHitFX;
    public AudioClip failFX;

    private SpriteRenderer mySpriteRenderer;
    private Rigidbody2D myRigidbody2D;
    private AudioSource myAudioSource;

    private bool isBallActive = false;


    public void LaunchBallFirstTime()
    {
        this.transform.parent = ArkanoidManager.Instance.gameContainer.transform;
        myRigidbody2D.isKinematic = false;
        myRigidbody2D.velocity = Vector2.up * speed + Vector2.right * 0.5f;
    }

    public void ResetBall()
    {
        if(myRigidbody2D == null) myRigidbody2D = GetComponent<Rigidbody2D>();  // Sentencia seguridad

        //this.transform.parent = racket.transform;
        myRigidbody2D.isKinematic = true;
        myRigidbody2D.velocity = Vector2.zero;
        myRigidbody2D.angularVelocity = 0f;
        transform.localPosition = new Vector2(0.27f, 0.35f);

        StartCoroutine("CheckBallLaunchTimed");
        isBallActive = false;
    }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = mySpriteRenderer.sprite;

        ResetBall();
    }

    private void OnEnable()
    {
        ResetBall();
    }

    IEnumerator CheckBallLaunchTimed()  /**/
    {
        yield return new WaitForSeconds(3);
        if (!isBallActive)
        {
            LaunchBallFirstTime();
            isBallActive = true;
        }
    }

    private void Update()
    {
        if (!isBallActive) 
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
            {
                LaunchBallFirstTime();
                isBallActive = true;
                StopCoroutine("CheckBallLaunchTimed");
            }
        }

        if(isBallActive)
        {
            LimitVelocity();
            CheckDirection();
        }
    }

    private void LimitVelocity()
    {
        if (myRigidbody2D.velocity.magnitude > speed)
        {
            myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * speed;
        }
    }

    private void CheckDirection()
    {
        Vector3 myVelo = myRigidbody2D.velocity.normalized;
        float minVelo = 0.1f;
        if (Mathf.Abs(myVelo.x) > minVelo && Mathf.Abs(myVelo.y) > minVelo) return;

        //Debug.Log("Info -> Ball velo: " + myVelo);
        float minFactor = 0.2f; // Minimo factor de velocidad horizontal o vertical.

        if (Mathf.Abs(myVelo.x) <= minVelo)
        {
            myVelo.x = CheckDirectionComponent(myVelo.x, minFactor);
            myRigidbody2D.velocity = myVelo * speed;
        }

        if (Mathf.Abs(myVelo.y) <= minVelo)   // if we hit the racket don't test vertical direction
        {
            myVelo.y = CheckDirectionComponent(myVelo.y, minFactor);
            myRigidbody2D.velocity = myVelo * speed;
        }
    }

    private float CheckDirectionComponent(float _velo, float minFactor)
    {
        //Debug.Log("Error -> Ball velo: " + _velo);
        int dir = _velo > 0 ? 1 : -1;
        if (_velo == 0)
            dir = Random.Range(0, 2) == 0 ? 1 : -1;

        return minFactor * dir;
    }


    void OnCollisionEnter2D (Collision2D col)
    {
        MakeHitSound(col.transform.tag);
        IlumBall();

        if (!col.gameObject.tag.Contains("Racket")) { return; }

        col.gameObject.SendMessage("IlumRacket");   // Ilumina la raqueta al chocar con la bola.

        //Debug.Log("1 > " + col.gameObject.name);

        // Calculamos el hit Factor (Factor desplazamiento en vertical).
        float x = GetHitFactor(this.transform.position, col.transform.position, col.collider.bounds.size.x);

        //Vector2 dir = col.gameObject.name.Contains("Down") ? new Vector2(x, 1).normalized : new Vector2(x, -1).normalized;
        Vector2 dir = new Vector2(x, 1).normalized;

        //Debug.Log("2 > " + dir);

        myRigidbody2D.velocity = dir * speed;
    }

    private float GetHitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        // 1 <-> En lo alto de la raqueta.
        //
        // 0 <-> En la mitad de la raqueta
        //
        // -1 <-> En la parte de abajo de la raqueta.

        return (ballPos.x - racketPos.x) / (racketWidth);
    }


    private void MakeHitSound(string tag)
    {
        if(tag.Contains("Racket"))
        {
            myAudioSource.PlayOneShot(racketHitFX);
        }
        else if (tag.Contains("Wall"))
        {
            myAudioSource.PlayOneShot(wallHitFX);
        }
        else if (tag.Contains("Block"))
        {
            myAudioSource.PlayOneShot(blockHitFX);
        }
    }

    public void PlayFailSoundFX()
    {
        if (failFX != null)
        {
            myAudioSource.PlayOneShot(failFX);
        }
    }

    public void IlumBall()
    {
        mySpriteRenderer.sprite = ilumSprite;
        StartCoroutine("_StopIlumBall");
    }

    IEnumerator _StopIlumBall()
    {
        yield return new WaitForSeconds(ilumTime);
        mySpriteRenderer.sprite = normalSprite;
    }
}
