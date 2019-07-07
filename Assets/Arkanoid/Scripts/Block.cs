using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite[] cracks;
    [Range(0, 5)] public int numCols = 1;     // Si numCols es cero, el bloque es indestructible.
    public int numPoints = 10;

    private int numTicks = 0;                 // Numero de toques que se le han dado a este bloque.
    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        numTicks++;
        ChangeCrack();

        if (numTicks < numCols) return;

        if (numCols != 0)
        {
            ArkanoidManager.Instance.AddScore(numPoints);
            PickupMngr.Instance.TryCreatePickup(this.transform.position);
            ArkanoidManager.Instance.DestroyBlock();
            Destroy(this.gameObject);
        }
    }

    private void ChangeCrack()
    {
        if (cracks.Length == 0) return; // Sentencia de seguridad
        if (numTicks >= numCols) return;    // Si se da este caso es porque el bloque se tiene que romper y destruir.

        mySpriteRenderer.sprite = cracks[numTicks - 1];
    }

}
