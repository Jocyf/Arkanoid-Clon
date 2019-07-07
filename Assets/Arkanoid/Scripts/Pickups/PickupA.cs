using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupA : MonoBehaviour
{
    public float fallSpeed = 1.0f;
    public Vector3 targetScl = Vector3.one;

    private Rigidbody2D myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = Vector2.down * fallSpeed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Contains("Racket")) return;

        other.transform.localScale = targetScl;
        PickupMngr.Instance.DestroyPickup(this.gameObject);
    }

}
