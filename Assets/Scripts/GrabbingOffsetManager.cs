using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingOffsetManager : MonoBehaviour {

    public Vector3 GrabOffset
    {
        get
        {
            return grabOffset;
        }
    }

    [SerializeField] Vector3 grabOffset;

}
