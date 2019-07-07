using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundDetector : MonoBehaviour
{

    void OnTriggerEnter2D (Collider2D other)
    {
		if(!other.tag.Contains("Ball")) { return; }

        other.SendMessage("PlayFailSoundFX");   // Lanza el sonido de fallo.
        ArkanoidManager.Instance.Die();
    }

}
