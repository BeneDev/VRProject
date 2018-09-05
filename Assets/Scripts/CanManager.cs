using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanManager : MonoBehaviour {

    float initialYPos;
    [SerializeField] float yOffsetToDestroy = 0.5f;

    private void Awake()
    {
        initialYPos = transform.position.y;
        InvokeRepeating("CheckYPos", 0f, 1f);
    }

    void CheckYPos()
    {
        if(transform.position.y < initialYPos - yOffsetToDestroy && transform.parent)
        {
            transform.parent.gameObject.GetComponent<CanTowerManager>().OnAllCansKnockedOver += Vanish;
            transform.parent = null;
        }
    }

    void Vanish()
    {
        Destroy(gameObject);
    }
}
