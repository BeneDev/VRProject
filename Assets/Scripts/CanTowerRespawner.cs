using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTowerRespawner : MonoBehaviour {

    [SerializeField] GameObject prefab;
    Vector3 childPos;

    private void Awake()
    {
        childPos = transform.GetChild(0).transform.position;
        InvokeRepeating("RespawnCanTower", 0f, 1f);
    }

    void RespawnCanTower()
    {
        if(transform.childCount <= 0f && prefab)
        {
            Instantiate(prefab, childPos, transform.rotation, transform);
        }
    }
}
