using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    private void Awake()
    {
        InvokeRepeating("CheckYPosition", 1f, 1f);
    }

    void CheckYPosition()
    {
        if(transform.position.y <= -50f)
        {
            Destroy(gameObject);
        }
    }
}
