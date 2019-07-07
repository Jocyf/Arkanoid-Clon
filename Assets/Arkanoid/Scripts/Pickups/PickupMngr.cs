using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMngr : MonoBehaviour
{

    public GameObject[] pickups;
    [Range(0, 100)] public int prob = 10;

    public List<GameObject> pickupList = new List<GameObject>();

    #region Singleton
    public static PickupMngr Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public void TryCreatePickup(Vector3 pos)
    {
        if (CkeckPickupCreation())
        {
            CreatePickup(pos);
        }
    }

    private bool CkeckPickupCreation()
    {
        /*bool nResult = false;

        int n = Random.Range(0, 100);
        if(n < prob)
        {
            nResult = true;
        }

        return nResult;*/

        return Random.Range(0, 101) < prob;
    }

    private void CreatePickup(Vector3 _pos)
    {
        int n = Random.Range(0, pickups.Length);
        GameObject obj = Instantiate(pickups[n], _pos, Quaternion.identity);
        pickupList.Add(obj);
    }

    public void DestroyPickup(GameObject _pickup)
    {
        pickupList.Remove(_pickup);
        Destroy(_pickup);
    }

    public void DestroyAllPickups()
    {
        for(int i = 0; i < pickupList.Count; i++)
        {
            Destroy(pickupList[i]);
        }

        pickupList.Clear();
    }

}
