using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    [SerializeField] Transform ballParent;

    int ballsInContainer = 0;
    [SerializeField] int minBalls;
    [SerializeField] int spawnCount = 3;
    [SerializeField] Collider checkingCollider;
    int ballLayer;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform spawnPoint;

    private void Awake()
    {
        ballLayer = LayerMask.NameToLayer("Grabable");
        InvokeRepeating("SpawnBalls", 0f, 1f);
    }

    void SpawnBalls()
    {
        if(ballsInContainer < minBalls)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if(ballParent)
                {
                    Instantiate(ballPrefab, spawnPoint.position + new Vector3(Random.Range(-checkingCollider.bounds.extents.x, checkingCollider.bounds.extents.x), Random.Range(-checkingCollider.bounds.extents.y, checkingCollider.bounds.extents.y), 0f), Quaternion.Euler(Vector3.zero), ballParent);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == ballLayer)
        {
            ballsInContainer++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == ballLayer)
        {
            ballsInContainer--;
        }
    }

}
