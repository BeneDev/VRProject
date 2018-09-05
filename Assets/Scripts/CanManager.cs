using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanManager : MonoBehaviour {

    void Vanish()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" && transform.parent)
        {
            transform.parent.gameObject.GetComponent<CanTowerManager>().OnAllCansKnockedOver += Vanish;
            transform.parent = null;
        }
    }
}
