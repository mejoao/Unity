using UnityEngine;
using System.Collections;

public class AirWolfCameraControl : MonoBehaviour {

	public Transform target;
	public bool      pause;
	
	public void Reset() { pause = false; }

	public void LateUpdate() {
		if (pause) return;
		
		// This is the projection of the target's up vector onto the World X-Z plane
		Vector3 dir        = target.up - Vector3.Dot(target.up, Vector3.up) * Vector3.up;
		transform.position = target.position - 
			                 dir * GameValues.AIR_WOLF_CAMERA_OFFSET_DISTANCE + 
				             new Vector3(0, GameValues.AIR_WOLF_CAMERA_HEIGHT, 0);
		transform.LookAt(target);
	}
}