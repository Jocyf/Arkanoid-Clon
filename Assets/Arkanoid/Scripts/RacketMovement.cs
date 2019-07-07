using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour
{
    public float speed = 30;
    public bool useMobileInput = true;

    public float sensibity = 0.4f;

    private Rigidbody2D myRigidbody2D;


    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        float h = 0;

        if (useMobileInput)
        {
            h = Input.GetAxis("Mouse X") * sensibity;
            if (h > 1)
                h = 1;
            else if (h < -1)
                h = -1;

            //Debug.Log("v: " + v.ToString() + " - Mouse Y: " + Input.GetAxis("Mouse Y").ToString());
        }
        else
        {
            h = Input.GetAxis("Horizontal");
        }

        myRigidbody2D.velocity = new Vector2(h, 0) * speed;
    }
}
