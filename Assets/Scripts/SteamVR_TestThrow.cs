﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using UnityEngine.XR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow : MonoBehaviour
{
	public GameObject prefab;
	public Rigidbody attachPoint;

    [SerializeField] Collider grabCollider;
    int grabableObjects;
 
    [SerializeField] Vector3 velMultiplier;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;

    GameObject possibleGrabObj;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
        grabableObjects = LayerMask.NameToLayer("Grabable");
	}

	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
            if(possibleGrabObj)
            {
                //var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
                SteamVR_Controller.Input((int)device.index).TriggerHapticPulse(ushort.MaxValue, Valve.VR.EVRButtonId.k_EButton_Max);

                var grabbedObject = possibleGrabObj;

                Vector3 offset = grabbedObject.transform.position - attachPoint.transform.position;

                grabbedObject.transform.position = attachPoint.transform.position + offset;

                joint = grabbedObject.AddComponent<FixedJoint>();
                joint.connectedBody = attachPoint;
            }
		}
		else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			var go = joint.gameObject;
			var rb = go.GetComponent<Rigidbody>();
			Object.DestroyImmediate(joint);
			joint = null;
            
			// We should probably apply the offset between trackedObj.transform.position
			// and device.transform.pos to insert into the physics sim at the correct
			// location, however, we would then want to predict ahead the visual representation
			// by the same amount we are predicting our render poses.

			var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            Vector3 throwVelo = origin.TransformVector(device.velocity);
            if (origin != null)
			{
                rb.velocity = new Vector3(throwVelo.x * (1f + (velMultiplier.x * (1 - (rb.mass / 100f)))), throwVelo.y * (1f + (velMultiplier.y * (1 - (rb.mass / 100f)))), throwVelo.z * (1f + (velMultiplier.z * (1 - (rb.mass / 100f)))));
				rb.angularVelocity = origin.TransformVector(device.angularVelocity);
			}
			else
			{
				rb.velocity = new Vector3(throwVelo.x * (1f + (velMultiplier.x * (1 - (rb.mass / 100f)))), throwVelo.y * (1f + (velMultiplier.y * (1 - (rb.mass / 100f)))), throwVelo.z * (1f + (velMultiplier.z * (1 - (rb.mass / 100f)))));
                rb.angularVelocity = device.angularVelocity;
			}

			rb.maxAngularVelocity = rb.angularVelocity.magnitude;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(grabableObjects == other.gameObject.layer)
        {
            possibleGrabObj = other.gameObject;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (grabableObjects == other.gameObject.layer)
        {
            possibleGrabObj = null;
        }
    }
}
