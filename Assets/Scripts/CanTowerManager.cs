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
        if(transform.childCount <= 0f)
        {
            DestroyAll();
        }
    }

    void DestroyAll()
    {
        if (OnAllCansKnockedOver != null)
        {
            OnAllCansKnockedOver();
        }
        Destroy(gameObject);
    }

}
