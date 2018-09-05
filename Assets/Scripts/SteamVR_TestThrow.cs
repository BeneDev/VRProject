//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow : MonoBehaviour
{
	public GameObject prefab;
	public Rigidbody attachPoint;

    [SerializeField] Vector3 velMultiplier;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			var newBall = GameObject.Instantiate(prefab);
			newBall.transform.position = attachPoint.transform.position;

			joint = newBall.AddComponent<FixedJoint>();
			joint.connectedBody = attachPoint;
		}
		else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			var go = joint.gameObject;
			var rb = go.GetComponent<Rigidbody>();
			Object.DestroyImmediate(joint);
			joint = null;
			Object.Destroy(go, 15.0f);

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
}
