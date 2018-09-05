using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTowerManager : MonoBehaviour {

    public event System.Action OnAllCansKnockedOver;

    private void Awake()
    {
        InvokeRepeating("CheckCanCount", 0f, 1f);
    }

    void CheckCanCount()
    {
        print(transform.childCount);
        if(transform.childCount <= 0f)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        if (OnAllCansKnockedOver != null)
        {
            OnAllCansKnockedOver();
        }
        Destroy(gameObject);
    }

}
