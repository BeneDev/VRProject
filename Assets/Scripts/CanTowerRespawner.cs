using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTowerRespawner : MonoBehaviour {

    [SerializeField] GameObject prefab;
    [SerializeField] GameObject existentCanTower;
    Vector3 canTowerPos;

    private void Awake()
    {
        InvokeRepeating("RespawnCanTower", 0f, 1f);
    }

    void RespawnCanTower()
    {
        if(!existentCanTower && prefab)
        {
            GameObject newCanTower = Instantiate(prefab, canTowerPos, Quaternion.Euler(Vector3.zero));
            existentCanTower = newCanTower;
        }
        else
        {
            canTowerPos = existentCanTower.transform.position;
        }
    }
}
