using UnityEngine;
using System.Collections;

public class MiniMapCameraControl : MonoBehaviour {

	public Transform target;


	public void LateUpdate() {
		transform.position = target.position + new Vector3 (0, GameValues.MINI_MAP_CAMERA_HEIGHT, 0);
		transform.rotation = target.rotation;
		transform.Rotate (90, 0, 0);

	}
}
